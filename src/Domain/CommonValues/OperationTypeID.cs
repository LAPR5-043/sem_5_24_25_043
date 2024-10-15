public class OperationTypeID{
    private long Id { get; set;}

    public OperationTypeID(long id){
        SetId(id);
    }

    public long GetId(){
        return Id;
    }


    private void SetId(long id){
        if (id < 0) {
            throw new ArgumentException("ID cannot be negative.");
        }
        this.Id = id;
    }



}