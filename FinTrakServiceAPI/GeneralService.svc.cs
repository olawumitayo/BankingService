using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FinTrakDAL;
using FinTrakLogic.Logic;

namespace FinTrakServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GeneralService" in code, svc and config file together.
    public class GeneralService : IGeneralService
    {
        QueryManager query = new QueryManager();
        readonly FinTrakDAL.FinTrakFundManagerEntities oEntities = new FinTrakDAL.FinTrakFundManagerEntities();
        public string VerifyCustomerAccount(string AccountNumber)
        {
            string result;
            result = query.VerifyCustomerAccount(AccountNumber);
            return result;
        }

        public decimal GetCustomerBalance(string AccountNumber)
        {
            decimal result = 0;
            result = query.GetCustomerBalance(AccountNumber);
            return result;
        }

        public bool isGLAccountValid(string AccountNumber)
        {
            bool result = false;
            result = query.isGLAccountValid(AccountNumber);
            return result;
        }

        public string PostTransactions(List<PostingDTO> lstPostingObject)
        {
            string result;
            result = query.PostTransactions(lstPostingObject);
            return result;
        }

        public string PostRemittance(List<PostingDTO> lstPostingObject)
        {
            string result;
            result = query.PostRemittance(lstPostingObject);
            return result;
        }

        //public  string TestPosting()
        //{
        //    string accountNumber = "0000053611";

        //   // PostingDTO[] listOfPostings = new PostingDTO[5];

        //    List<PostingDTO> lst = new List<PostingDTO>();

        //    PostingDTO oPostingDTO = new PostingDTO();
        //    oPostingDTO.Amount = 100;
        //    oPostingDTO.AppID = "Internet Banking";
        //    oPostingDTO.ApprovedBy = "Self";
        //    oPostingDTO.BatchRef = "sdsd";
        //    oPostingDTO.BrCode = "101";
        //    oPostingDTO.CrAccount = "0000066657";
        //    oPostingDTO.CrNarration = "By Self from Internet Banking";
        //    oPostingDTO.DrAccount = accountNumber;
        //    oPostingDTO.DrNarration = "Transfer to Myself from internet Banking.";
        //    oPostingDTO.PostedBy = "Self : Internet Banking";
        //    oPostingDTO.PostType = 1;
        //    oPostingDTO.TransactionType = 1;
        //    oPostingDTO.ValueDate = GetCurrentSystemDate();
        //    oPostingDTO.MISCode = "";

        //    lst.Add(oPostingDTO);
        //    //listOfPostings[0] = oPostingDTO;

        //    string postingStatus = PostTransactions(lst);

        //    return postingStatus;
        //}

        public string GetCompanyName()
        {
            string result;
            result = query.GetCompanyName();
            return result;
        }

        public string StopCheque(string AccountNumber, string ChequeNo, string Comment, DateTime TransactionDate)
        {
            string result;
            result = query.StopCheque(AccountNumber, ChequeNo, Comment, TransactionDate);
            return result;
        }

        public string ConfirmCheque(string AccountNumber, string ChequeNo, decimal amount, string BeneficiaryName, string PhoneNumber)
        {
            string result;
            result = query.ConfirmCheque(AccountNumber, ChequeNo, amount, BeneficiaryName, PhoneNumber);
            return result;
        }

        public string ChequeBookRequisition()
        {
            string result;
            result = query.ChequeBookRequisition();
            return result;
        }

        public bool isCustomerAccountValid(string AccountNumber)
        {
            bool result = false;
            result = query.isCustomerAccountValid(AccountNumber);
            return result;
        }

        public List<ChartOfAccountDTO> GetChartOfAccount(string coyCode)
        {
            return query.GetChartOfAccount(coyCode);
        }

        public DateTime GetCurrentSystemDate()
        {
            DateTime result;
            result = query.GetCurrentSystemDate();
            return result;
        }


        #region Revenue

        public List<Teller> GetTellersList()
        {
            List<Teller> tellerList = new List<Teller>();
            foreach (var item in query.GetTellersLogList().Where(o => o.Active == true))
            {
                Teller teller = new Teller()
                {
                    name = item.Remark,
                    branchcode = item.BranchID,
                    tillledger = item.AccountID,
                    staffid = item.Teller
                };
                tellerList.Add(teller);
            }
            return tellerList;

        }

      
        #endregion

        #region Internet Banking

        EBankingRepository oRepository = new EBankingRepository();

        #region Remittance
        public ChurchMapping GetChurchMapping(string customerCode)
        {
            try
            {

                return oRepository.GetChurchMapping(customerCode);

            }
            catch (Exception)
            {

                return new ChurchMapping();
            }
        }

        public List<RemittanceAccounts> ListRemittanceAccount(string customerCode)
        {
            try
            {

                return oRepository.ListRemittanceAccount(customerCode);
            }
            catch (Exception)
            {
                return new List<RemittanceAccounts>();
            }
        }
        #endregion



        #region Customer Account Management
        public string GetCustomeCode(string accountNumber)
        {
            if (!string.IsNullOrEmpty(accountNumber))
            {
                return oRepository.FetchCustomerCode(accountNumber);
            }

            return String.Empty;
        }

        public CustomerDetails GetCustomerDetails(string accountNumber)
        {

            return oRepository.GetCustomer(accountNumber);
        }

        public CustomerDetails GetCustomerDetailsForBeneficiary(string accountNumber)
        {

            return oRepository.GetCustomerForBeneficiary(accountNumber);
        }

       

        public List<CustomerAccounts> GetAllCustomerAccounts(string customerCode)
        {
            if (customerCode == "")
                return new List<CustomerAccounts>();

            return oRepository.AllCustomerAccount(customerCode);
        }

        public List<sp_Statement_Result> GenerateStatement(string customerCode, string companyCode,
            string branchCode, string accountNo, string startDate, string endDate, string accountName,int skip, int take)
        {
           var statement = oRepository.GetAccountStatementDetails(customerCode, companyCode, branchCode, accountNo, startDate, endDate,
                accountName, skip,  take);

           return statement;
        }

        #endregion

        #region Investment
        public CreateAccountResponse CreateAccountOnline(CreateAccountRequest request)
        {
            CreateAccountResponse result;
            result = oRepository.CreateAccount(request);
            return result;
        }
        public List<InvestmentDetails> GetAllDealsPerCustomer(string AccountNumber)
        {
            //var customerCode = from account in oEntities.ts_Banking_CASA
            //                   where account.ProductAcctNo == AccountNumber
            //                   select account;
            if (AccountNumber != "")
                return oRepository.GetAllDealsPerCustomer(AccountNumber);

            return new List<InvestmentDetails>();
        }
        public bool IsLoanAdded(LoanDetails AddLoan)
        {
            bool result = false;
            result = oRepository.IsLoanAdded(AddLoan);

            return result;
        }
        public InvestmentDetails GetDealInfo(string dealID)
        {
            if (dealID != "")
                return oRepository.GetInvestmentDetails(dealID);

            return new InvestmentDetails();
        }

        #endregion

        #region Loan

        public List<LoanProductDetails> GetAllLoanProducts()
        {
            return oRepository.GetAllLoanProducts();
        }
        public List<RateFee> GetRateFee(decimal amount)
        {
            return oRepository.GetRateFee(amount);
        }
        public decimal GetInvestmentBalance(string AccountNumber)
        {
            return oRepository.GetInvestmentBalance(AccountNumber);
        }
        public List<LoanDetails> GetCustomerLoanProducts(string customerCode)
        {
            return oRepository.GetCustomerLoan(customerCode);
        }
        public List<LoanScheduleDetails> GetAllLoanSchedule(
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
             )
        {

            var allSchedule = oRepository.GetLoanSchedule(Tenor, interestRate, principalBalance, startdate, moratorium,
                 numOfInstalment, CustCode, Ref, "", "", "", "", scheduleMethod, "", CustomerName, "", Frequency, 0, 0,
                 TenorMode, WithInterest,
                 Ref, "", FirstPayDate, Fee, freqOfFee, SetEqualDate, TerminalDate, false, PrincipalFrequency,
                 FixedPrincipal);

            var listScheduleDetails = new List<LoanScheduleDetails>();

            foreach (var _schedule in allSchedule)
            {
                var scheduleDetails = new LoanScheduleDetails
                {
                    CummulativeInterest = _schedule.CummulativeInterest.ToString(),
                    CustCode = _schedule.CustCode,
                    CummulativePrinRepyt = _schedule.CummulativePrinRepyt.ToString(),
                    EndBalance = _schedule.EndBalance.ToString(),
                    FeeCharged = _schedule.FeeCharged.ToString(),
                    InterestAccrual = _schedule.InterestAccrual.ToString(),
                    InterestPayment = _schedule.InterestPayment.ToString(),
                    Month = _schedule.Month.ToString(),
                    NextPayDay = _schedule.NextPayDay.ToString(),
                    NoOfDays = _schedule.NoOfDays.ToString(),
                    PrincipalBalance = _schedule.PrincipalBalance.ToString(),
                    PrincipalRepayment = _schedule.PrincipalRepayment.ToString(),
                    TotalRepayment = _schedule.TotalRepayment.ToString(),
                    productAcctNo = _schedule.productAcctNo
                };

                listScheduleDetails.Add(scheduleDetails);
            }

            return listScheduleDetails;
        }


        public List<ScheduleMethodDetails> GetLoanScheduleMethod()
        {
           var listOfSchedules  =  oRepository.GetLoanScheduleMethod();
            var listScheduleMethods = new List<ScheduleMethodDetails>();

           foreach (var method in listOfSchedules)
           {
               var oScheduleMethods = new ScheduleMethodDetails
               {
                    Description = method.Description,
                    Id =  method.ID,
                    ScheduleMethod = method.SchMethod
               };

               listScheduleMethods.Add(oScheduleMethods);

           }

            return listScheduleMethods;
        }

        public List<PaymentModeDetails> GetPaymentModes()
        {
            return oRepository.GetPaymentModes();
        }

        #endregion


        #region NIP

       public bool NIP_AmountFreeze(string accountNumber, string amount, string narration)
       {
          var status = oRepository.AmountFreeze(accountNumber, amount, narration);
            return status;
        }

        public bool NIP_AmountUnFreeze(string accountNumber, string amount, string narration)
        {
            var status = oRepository.AmountUnFreeze(accountNumber, amount, narration);

            return status;
        }

        public bool NIP_AccountFreeze(string accountNumber)
        {
            var status = oRepository.NIP_AccountFreeze(accountNumber);

            return status;
        }

        public bool NIP_AccountUnFreeze(string accountNumber)
        {
            var status = oRepository.NIP_AccountUnFreeze(accountNumber);

            return status;
        }


        public bool NIP_MandateAdvice(string accountNumber)
        {
            return true;
        }
        #endregion




        #endregion

    }
}
