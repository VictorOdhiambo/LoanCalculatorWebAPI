using DomainLayer_loanCalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer_loanCalc
{
    public class LoanRepo : ILoanRepo
    {
        private static readonly int RATE_FLAT = 18;
        private static readonly int RATE_REDUCING = 25;
        private static readonly int EXCISE_DUTY = 20;
        private static readonly int PROCESSING_FEE = 3;
        public List<ResponseBody> CalculateInterestUsingFlatRate(double amount, int period, double frequency, DateTime startDate)
        {
            List<ResponseBody> loanPayments = new List<ResponseBody>();

            double principal = Math.Round(CalculateTotalRepaymentAmount(amount), 2);
            double annualInterestRate = RATE_FLAT;
            int loanPeriodInYears = period;

            // Convert annual interest rate to frequency rate
            double interestRate = annualInterestRate / 100 / frequency;

            // Calculate the total number of installments (quarters | semi-anual | yearly)
            int totalInstallments = (int)(loanPeriodInYears * frequency);

            // Calculate the frequency interest and installment using the Flat Rate method
            double interest = principal * interestRate;
            double totalPaymentPerFreq = principal + interest;


            loanPayments.Add(new ResponseBody
            {
                PaymentDate = startDate.ToString("yyyy-MM-dd"),
                RemainingPrincipal = principal,
                Interest = interest,
                EMI = totalPaymentPerFreq,
                TotalRepaymentAmount = Math.Round(CalculateTotalRepaymentAmount(amount), 2),
                AnnualRate = RATE_FLAT,
                ProcessingFee = PROCESSING_FEE
            });

            if (frequency == 12)
            {
                frequency = 1;
            }
            else if (frequency == 1)
            {
                frequency = 12;
            }

            // Print the interest and installment for each quarter
            double remainingPrincipal = principal;
            for (int freq = 1; freq <= totalInstallments; freq++)
            {
                startDate = startDate.AddMonths((int)(frequency));

                loanPayments.Add(new ResponseBody
                {
                    PaymentDate = startDate.ToString("yyyy-MM-dd"),
                    RemainingPrincipal = principal,
                    Interest = interest,
                    EMI = totalPaymentPerFreq,
                    PercentagePaid = CalculatePercentagePaid(totalInstallments, freq),
                });

                remainingPrincipal -= totalPaymentPerFreq - interest;
            }


            return loanPayments;
        }

        public List<ResponseBody> CalculateInterestUsingReducingBalance(double amount, int period, double frequency, DateTime startDate)
        {
            List<ResponseBody> loanPayments = new List<ResponseBody>();

            double principal = Math.Round(CalculateTotalRepaymentAmount(amount), 2);
            double annualInterestRate = RATE_REDUCING;
            int loanPeriodInYears = period;


            // Convert annual interest rate to monthly rate
            double interestRate = annualInterestRate / 100 / frequency;

            // determine months to add to the current date
            if (frequency == 12)
            {
                frequency = 1;
            }
            else if (frequency == 1)
            {
                frequency = 12;
            }

            int totalInstallments = (int)(loanPeriodInYears * (12 /frequency));

            double initialInterest = (principal * loanPeriodInYears * annualInterestRate / 100) / totalInstallments;

            // Calculate the Installment Amount (EMI)
            double emi = CalculateEMI(principal, interestRate, totalInstallments);

            loanPayments.Add(new ResponseBody
            {
                PaymentDate = startDate.ToString("yyyy-MM-dd"),
                RemainingPrincipal = principal,
                EMI = Math.Round(emi, 2),
                Interest = Math.Round(initialInterest, 2),
                TotalRepaymentAmount = Math.Round(CalculateTotalRepaymentAmount(amount), 2),
                AnnualRate = RATE_REDUCING,
                ProcessingFee = PROCESSING_FEE
            });

            double totalRepayment = 0;

            // Calculate and print the interest and installment
            double remainingPrincipal = principal;
            for (int month = 1; month <= totalInstallments; month++)
            {
                double interestAmount = Math.Round(remainingPrincipal, 2) * Math.Round(interestRate, 2);
                double installmentAmount = emi - interestAmount;
                remainingPrincipal -= installmentAmount;

                double monthlyInterest = remainingPrincipal * (interestRate / 100) / 12;
                double monthlyInstallment = principal / frequency;
                totalRepayment += monthlyInterest + monthlyInstallment;

                startDate = startDate.AddMonths((int)(frequency));

                loanPayments.Add(
                    new ResponseBody
                    {
                        PaymentDate = startDate.ToString("yyyy-MM-dd"),
                        RemainingPrincipal = (remainingPrincipal < 0) ? 0 : Math.Round(remainingPrincipal, 2),
                        Interest = Math.Round(interestAmount, 2),
                        EMI = Math.Round(installmentAmount, 2),
                        PercentagePaid = CalculatePercentagePaid(totalInstallments, month),
                    }
                    );

            }
            return loanPayments;
        }

        private double CalculateTotalRepaymentAmount(double principal)
        {
            principal += (PROCESSING_FEE * principal) / 100;
            return principal;
        }
        private double CalculatePercentagePaid(int duration, int currentMonth)
        {
            return (currentMonth * 100) / duration;
        }

        private double CalculateEMI(double principal, double monthlyInterestRate, int totalInstallments)
        {
            double numerator = principal * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalInstallments);
            double denominator = Math.Pow(1 + monthlyInterestRate, totalInstallments) - 1;

            return numerator / denominator;
        }
    }
}
