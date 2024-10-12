public class OperationTypeID{
    private long id { get; set;}

    public OperationTypeID(long id){
        setID(id);
    }

    public long getID(){
        return id;
    }


    private void setID(long id){
        if (id < 0) {
            throw new ArgumentException("ID cannot be negative.");
        }
        this.id = id;
    }



}