using ItServiceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ItServiceApp.Test
{
    public class PaymentTest
    {
        private readonly IPaymentService _paymentService;
        public PaymentTest(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Fact]
        public void CheckInstallments()
        {
            var binNumbers = new string[]
            {"4543590000000006","4157920000000002","374427000000003","4766620000000001"};
            foreach (var bin in binNumbers)
            {
                var result = _paymentService.CheckInstallments(bin, 1000);
            }
            Assert.Equal(true, true);

        }
    }
}
