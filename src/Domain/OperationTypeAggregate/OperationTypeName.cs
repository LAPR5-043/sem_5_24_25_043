public class OperationTypeName{

    public string name { get; set; }

    public OperationTypeName(string name){
        setName(name);
    }

    public string getName(){
        return name;
    }

    private void setName(string name){
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentException("Name cannot be null or empty.");
        }
        this.name = name;
    }
}

