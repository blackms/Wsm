using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Wsm.Wcf.Contracts
{
    [DataContract]
    public class CalculatorFault
    {
        [DataMember]
        public string Message { get; set; }
    }

    [ServiceContract]
    public interface ICalculator
    {
        // Synchronous equivalent:
        //  [OperationContract]
        //  [FaultContract(typeof(CalculatorFault))]
        //  uint Divide(uint numerator, uint denominator);

        [OperationContract]
        [FaultContract(typeof(CalculatorFault))]
        Task<int> DivideAsync(int numerator, int denominator);

        [OperationContract(IsOneWay = true)]
        void WaitBlocking();

    }
}
