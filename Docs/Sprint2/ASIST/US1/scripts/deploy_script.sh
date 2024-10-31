#!/bin/bash


repo_path="/root/project/Hospital_3D_Model/main"
production_path="/root/production"
logs_file="/root/deployScripts/logs.txt"

cd "$repo_path" || exit

log() {
    local timestamp=$(date +"[%d-%m-%y %T] - ")
    echo "$timestamp $1" >> "$logs_file"
}

exec >> "$logs_file" 2>&1

if [ "$(git fetch && git status -uno | grep 'Your branch is behind')" > /dev/null ]; then

    log "Changes detected, pulling repo."
    
    git pull https://$GIT_USER:$GIT_TOKEN@github.com/username/repository.git main

    log "Pull finished."

    npm install
    npm run build
    if [ $? -eq 0 ]; then

        log "Build successful, copying files."
        cp -r "$repo_path" "$production_path"
        log "Files copied."

        APP_PID=$(pgrep -f "react-scripts start")

        if [ -n "$APP_PID" ]; then
            log "Restarting app."
            kill -9 "$APP_PID"
        fi

        cd "$production_path" || exit
        kill -9 $(lsof -t -i :8080)
        npm install
        npm start -- --host 0.0.0.0 &
        log "App running."

        exit 0
    else
        log "Build failed."
    fi
else
    log "No changes detected."
fi
