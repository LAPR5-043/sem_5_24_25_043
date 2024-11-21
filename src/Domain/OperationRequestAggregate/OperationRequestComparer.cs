using System;
using System.Collections.Generic;
using Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;

public class OperationRequestComparer : IComparer<OperationRequest>
{
    public int Compare(OperationRequest x, OperationRequest y)
    {
        if (x.priority != y.priority)
        {
            return PriorityExtensions.ToInt(y.priority).CompareTo(PriorityExtensions.ToInt(x.priority)); // Prioridade mais alta primeiro
        }else if ( x.deadlineDate.CompareTo(y.deadlineDate) == 0)
        {
            return x.operationRequestID.CompareTo(y.operationRequestID); // ID mais baixo primeiro
        }
        
        return x.deadlineDate.CompareTo(y.deadlineDate); // Data limite mais próxima primeiro
    }
}
