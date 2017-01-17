using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wsm.Wcf.Contracts;

namespace Wsm.Wcf.Concrete
{
    public class CalculatorService : ICalculator
    {
        public async Task<int> DivideAsync(int numerator, int denominator)
        {
            try
            {
                await Task.Delay(5000);

                return await Task<int>.Run( () =>                 
                {                
                    return numerator / denominator;
                });
            }
            catch (DivideByZeroException)
            {
                throw new FaultException<CalculatorFault>(new CalculatorFault { Message = "Undefined result" });
            }
        }


        public void WaitBlocking()
        {
            Thread.Sleep(10000);
        }
    }
}
