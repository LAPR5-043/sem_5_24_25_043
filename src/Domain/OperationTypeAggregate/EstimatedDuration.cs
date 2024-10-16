public class EstimatedDuration{

    public int hours { get; private set;}
    public int minutes { get; private set;}
    
    public EstimatedDuration(){}
    public EstimatedDuration(int hours, int minutes){
        setHours(hours);
        setMinutes(minutes);

    }

    public int getHours(){
        return hours;
    }

    public int getMinutes(){

        return minutes;

    }

    private void setHours(int hours){
        if (hours < 0 ) {
            throw new ArgumentException("Hours cannot be negative.");
        }
        this.hours = hours;
    }

    private void setMinutes(int minutes){
        if (minutes < 0) {
            throw new ArgumentException("Minutes cannot be negative.");
        }
        if ( minutes > 59){
            throw new ArgumentException("Minutes cannot be greater than 59.");
        }
        this.minutes = minutes;

    }        
}    
