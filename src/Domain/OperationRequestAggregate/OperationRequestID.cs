using Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.Shared;

namespace sem_5_24_25_043.Domain.OperationRequestAggregate
{
    /// <summary>
    /// Value object representing the ID of an operation request.
    /// </summary>
    public class OperationRequestID : EntityId
    {
        public string ID { get; set; }

        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        /// <param name="ID"></param>
        public OperationRequestID(string ID) : base(ID)
        {
            this.ID = ID;
        }
        public OperationRequestID() : base(null)
        {
        }

        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (OperationRequestID)obj;
            return ID == other.ID;
        }
        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID;
        }
        /// <summary>
        /// Override of the createFromString method.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override object createFromString(string text)
        {
            return new String(text);
        }
        /// <summary>
        /// Override of the AsString method.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string AsString()
        {
            return ID;
        }
    }
}