using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FinTrakLogic.Logic;
using FinTrakDAL;

namespace FinTrakServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGeneralService" in both code and config file together.
    [ServiceContract]
    public interface IGeneralService
    {
        [OperationContract]
        ChurchMapping GetChurchMapping(string customerCode);

        [OperationContract]
        List<RemittanceAccounts> ListRemittanceAccount(string customerCode);

        [OperationContract]
        string VerifyCustomerAccount(string AccountNumber);

        [OperationContract]
        decimal GetCustomerBalance(string AccountNumber);
        [OperationContract]
        decimal GetInvestmentBalance(string AccountNumber);
        [OperationContract]
        bool isGLAccountValid(string AccountNumber);

        [OperationContract]
        string PostTransactions(List<PostingDTO> lstPostingObject);

        [OperationContract]
        string PostRemittance(List<PostingDTO> lstPostingObject);

        //[OperationContract]
        //string TestPosting();

        [OperationContract]
        string GetCompanyName();

        [OperationContract]
        string StopCheque(string AccountNumber, string ChequeNo, string Comment, DateTime TransactionDate);

        [OperationContract]
        string ConfirmCheque(string AccountNumber, string ChequeNo, decimal amount, string BeneficiaryName, string PhoneNumber);

        [OperationContract]
        string ChequeBookRequisition();

        [OperationContract]
        bool isCustomerAccountValid(string AccountNumber);

        [OperationContract]
        List<ChartOfAccountDTO> GetChartOfAccount(string coyCode);
        [OperationContract]
        List<RateFee> GetRateFee(decimal amount);
        [OperationContract]
        DateTime GetCurrentSystemDate();

        #region Teller
        [OperationContract]
        List<Teller> GetTellersList();
        #endregion

        #region Internet Banking 

        #region Customer Account Management
        [OperationContract]
        string GetCustomeCode(string accountNumber);
        [OperationContract]
        bool IsLoanAdded(LoanDetails AddLoan);
            [OperationContract]
        CreateAccountResponse CreateAccountOnline(CreateAccountRequest request);

        [OperationContract]
        CustomerDetails GetCustomerDetails(string accountNumber);

        [OperationContract]
        CustomerDetails GetCustomerDetailsForBeneficiary(string accountNumber);
       

        [OperationContract]
        List<CustomerAccounts> GetAllCustomerAccounts(string customerCode);

        [OperationContract]
        List<sp_Statement_Result> GenerateStatement(string customerCode, string companyCode,
            string branchCode, string accountNo, string startDate, string endDate, string accountName, int skip, int take);

        #endregion

        #region Investment
        [OperationContract]
        List<InvestmentDetails> GetAllDealsPerCustomer(string AccountNumber);

        [OperationContract]
        InvestmentDetails GetDealInfo(string dealID);

        #endregion

        #region Loan

        [OperationContract]
        List<LoanProductDetails> GetAllLoanProducts();

        [OperationContract]
        List<LoanDetails> GetCustomerLoanProducts(string customerCode);

        [OperationContract]
        List<LoanScheduleDetails> GetAllLoanSchedule(
             int Tenor,
            decimal interestRate,
            float principalBalance,
            DateTime startdate, //Effective Date
            int moratorium, //Moratorium - Grace Period
            float numOfInstalment,
            string CustCode,
            int scheduleMethod, //Schedule Method ID 1. Anuity, 2. Reducing Balance
            string CustomerName, //Customer Name
            decimal Frequency, //
            int TenorMode, //Day Month Year
            bool WithInterest,
            string Ref, //
            DateTime FirstPayDate, //
            decimal Fee,
            int freqOfFee,
            bool SetEqualDate,
            DateTime TerminalDate,
            decimal PrincipalFrequency,
            decimal FixedPrincipal //0
            );

        [OperationContract]
        List<ScheduleMethodDetails> GetLoanScheduleMethod();

        [OperationContract]
        List<PaymentModeDetails> GetPaymentModes();

        #endregion

        #region NIP

        [OperationContract]
        bool NIP_AmountFreeze(string accountNumber, string amount, string narration);

        [OperationContract]
        bool NIP_AmountUnFreeze(string accountNumber, string amount, string narration);

        [OperationContract]
        bool NIP_AccountFreeze(string accountNumber);

        [OperationContract]
        bool NIP_AccountUnFreeze(string accountNumber);

        [OperationContract]
        bool NIP_MandateAdvice(string accountNumber);

        #endregion

        #endregion
    }
}
