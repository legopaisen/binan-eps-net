using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using Modules.Utilities;

namespace Modules.Reports
{
    public class Application : FormReportClass
    {
        string pNew = string.Empty;
        string pRen = string.Empty;
        string pAppLN = string.Empty;
        string pAppFN = string.Empty;
        string pAppMI = string.Empty;
        string pAddress = string.Empty;
        string pAppTIN = string.Empty;
        string pLotNo = string.Empty;
        string pBlkNo = string.Empty;
        string pTCT = string.Empty;
        string pStreet = string.Empty;
        string pBrgy = string.Empty;
        string pCity = string.Empty;
        string pScopeNew = string.Empty;
        string pScopeErection = string.Empty;
        string pScopeAdd = string.Empty;
        string pScopeReno = string.Empty;
        string pScopeConvert = string.Empty;
        string pScopeRepair = string.Empty;
        string pScopeMove = string.Empty;
        string pScopeRaise = string.Empty;
        string pScopeAcce = string.Empty;
        string pScopeAlter = string.Empty;
        string pScopeOthers = string.Empty;
        string pFormOwn = string.Empty;
        string pUseA = string.Empty;
        string pUseB = string.Empty;
        string pUseC = string.Empty;
        string pUseD = string.Empty;
        string pUseE = string.Empty;
        string pUseF = string.Empty;
        string pUseG = string.Empty;
        string pUseH = string.Empty;
        string pUseI = string.Empty;
        string pUseJ = string.Empty;
        string pUseO = string.Empty;
        string pOccupancy = string.Empty;
        string pNoUnits = string.Empty;
        string pFlrArea = string.Empty;
        string pEstCost = string.Empty;
        string pPropConstDate = string.Empty;
        string pCompDate = string.Empty;
        string pArchEngr = string.Empty;
        string pPRC = string.Empty;
        string pPTR = string.Empty;
        string pTIN = string.Empty;
        string pLotOwner = string.Empty;

        public Application(frmReport Form) : base(Form)
        { }

        public override void LoadForm()
        {
            CreateParameters();
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
            new Microsoft.Reporting.WinForms.ReportParameter("pARN",AN),
            new Microsoft.Reporting.WinForms.ReportParameter("pNew",pNew),
            new Microsoft.Reporting.WinForms.ReportParameter("pRen",pRen),
            new Microsoft.Reporting.WinForms.ReportParameter("pAppLN",pAppLN),
            new Microsoft.Reporting.WinForms.ReportParameter("pAppFN",pAppFN),
            new Microsoft.Reporting.WinForms.ReportParameter("pAppMI",pAppMI),
            new Microsoft.Reporting.WinForms.ReportParameter("pAddress", pAddress),
            new Microsoft.Reporting.WinForms.ReportParameter("pAppTIN",pAppTIN),
            new Microsoft.Reporting.WinForms.ReportParameter("pLotNo",pLotNo),
            new Microsoft.Reporting.WinForms.ReportParameter("pBlkNo",pBlkNo),
            new Microsoft.Reporting.WinForms.ReportParameter("pTCT",pTCT),
            new Microsoft.Reporting.WinForms.ReportParameter("pStreet",pStreet),
            new Microsoft.Reporting.WinForms.ReportParameter("pBrgy",pBrgy),
            new Microsoft.Reporting.WinForms.ReportParameter("pCity",pCity),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeNew",pScopeNew),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeErection",pScopeErection),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeAdd",pScopeAdd),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeReno",pScopeReno),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeConvert",pScopeConvert),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeRepair",pScopeRepair),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeMove",pScopeMove),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeRaise",pScopeRaise),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeAcce",pScopeAcce),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeAlter",pScopeAlter),
            new Microsoft.Reporting.WinForms.ReportParameter("pScopeOthers",pScopeOthers),
            new Microsoft.Reporting.WinForms.ReportParameter("pFormOwn",pFormOwn),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseA",pUseA),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseB",pUseB),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseC",pUseC),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseD",pUseD),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseE",pUseE),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseF",pUseF),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseG",pUseG),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseH",pUseH),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseI",pUseI),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseJ",pUseJ),
            new Microsoft.Reporting.WinForms.ReportParameter("pUseO",pUseO),
            new Microsoft.Reporting.WinForms.ReportParameter("pOccupancy",pOccupancy),
            new Microsoft.Reporting.WinForms.ReportParameter("pNoUnits",pNoUnits),
            new Microsoft.Reporting.WinForms.ReportParameter("pFlrArea",pFlrArea),
            new Microsoft.Reporting.WinForms.ReportParameter("pEstCost",pEstCost),
            new Microsoft.Reporting.WinForms.ReportParameter("pPropConstDate",pPropConstDate),
            new Microsoft.Reporting.WinForms.ReportParameter("pCompDate",pCompDate),
            new Microsoft.Reporting.WinForms.ReportParameter("pArchEngr",pArchEngr),
            new Microsoft.Reporting.WinForms.ReportParameter("pPRC",pPRC),
            new Microsoft.Reporting.WinForms.ReportParameter("pPTR",pPTR),
            new Microsoft.Reporting.WinForms.ReportParameter("pTIN",pTIN),
            new Microsoft.Reporting.WinForms.ReportParameter("pLotOwner",pLotOwner)

            };
            ReportForm.reportViewer1.LocalReport.SetParameters(para);
        }

        private void CreateParameters()
        {
            pNew = " ";
            pRen = " ";
            pAppLN = " ";
            pAppFN = " ";
            pAppMI = " ";
            pAddress =  " ";
            pAppTIN = "";
            pLotNo = " ";
            pBlkNo = " ";
            pTCT = " ";
            pStreet = " ";
            pBrgy = " ";
            pCity = " ";
            pScopeNew = " ";
            pScopeErection = " ";
            pScopeAdd = " ";
            pScopeReno = " ";
            pScopeConvert = " ";
            pScopeRepair = " ";
            pScopeMove = " ";
            pScopeRaise = " ";
            pScopeAcce = " ";
            pScopeAlter = " ";
            pScopeOthers = " ";
            pFormOwn = " ";
            pUseA = " ";
            pUseB = " ";
            pUseC = " ";
            pUseD = " ";
            pUseE = " ";
            pUseF = " ";
            pUseG = " ";
            pUseH = " ";
            pUseI = " ";
            pUseJ = " ";
            pUseO = " ";
            pOccupancy = " ";
            pNoUnits = " ";
            pFlrArea = " ";
            pEstCost = " ";
            pPropConstDate = " ";
            pCompDate = " ";
            pArchEngr = " ";
            pPRC = " ";
            pPTR = " ";
            pTIN = " ";
            pLotOwner = " ";

            var db = new EPSConnection(dbConn);

            var result = (dynamic)null;
            string strWhereCond = string.Empty;
            string sScope = string.Empty;
            int iBldgNo = 0;

            strWhereCond = $" where arn = '{AN}' and main_application = '{1}'";

            if (ReportForm.ReportName == "Application")
            {
                result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
                
                         select a;
            }
            else
            {
                result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;
            }
            foreach (var item in result)
            {
                if (item.STATUS_CODE == "NEW")
                    pNew = "x";
                else
                    pRen = "x";

                Accounts account = new Accounts();
                account.GetOwner(item.PROJ_OWNER);
                pAppLN = account.LastName;
                pAppFN = account.FirstName;
                pAppMI = account.MiddleInitial;
                pAddress = account.CompleteAddress;
                pAppTIN = account.TIN;
                pLotNo = item.PROJ_LOT_NO;
                pBlkNo = item.PROJ_BLK_NO;
                pStreet = item.PROJ_ADDR;
                pBrgy = item.PROJ_BRGY;
                pCity = item.PROJ_CITY;
                sScope = item.SCOPE_CODE;
                GetScopeItem(sScope);
                //note pending setting values for Use of occupancy

                iBldgNo = item.BLDG_NO;
                GetBuilding(iBldgNo);

                try
                {
                    pPropConstDate = item.PROP_START.ToShortDateString();
                }
                catch { }
                try
                {
                    pCompDate = item.PROP_COMPLETE.ToShortDateString();
                }
                catch { }

                string sEngr = string.Empty;
                string sArch = string.Empty;
                string sEPRC = string.Empty;
                string sEPTR = string.Empty;
                string sETIN = string.Empty;
                string sAPRC = string.Empty;
                string sAPTR = string.Empty;
                string sATIN = string.Empty;

                sEngr = item.ENGR_CODE;
                sEngr = sEngr?.Split('|')[0]; //AFM 20211104 get first id if multiple engr applied
                sArch = item.ARCHITECT;
                sArch = sArch?.Split('|')[0]; //AFM 20211104 get first id if multiple engr applied

                Engineers engineer = new Engineers();
                engineer.GetOwner(sEngr);
                sEngr = engineer.OwnerName;
                sEPRC = engineer.PRC;
                sEPTR = engineer.PTR;
                sETIN = engineer.TIN;

                engineer.GetOwner(sArch);
                sArch = engineer.OwnerName;
                sAPRC = engineer.PRC;
                sAPTR = engineer.PTR;
                sATIN = engineer.TIN;

                pArchEngr = sEngr.Trim();
                if (!string.IsNullOrEmpty(sEngr) && !string.IsNullOrEmpty(sArch))
                    pArchEngr += "/";
                pArchEngr += sArch;

                pPRC = sEPRC;
                if (!string.IsNullOrEmpty(sEPRC) && !string.IsNullOrEmpty(sAPRC))
                    pPRC += "/";
                pPRC += sAPRC;

                pPTR = sEPTR;
                if (!string.IsNullOrEmpty(sEPTR) && !string.IsNullOrEmpty(sAPTR))
                    pPTR += "/";
                pPTR += sAPTR;

                pTIN = sETIN;
                if (!string.IsNullOrEmpty(sETIN) && !string.IsNullOrEmpty(sATIN))
                    pTIN += "/";
                pTIN += sATIN;

                account.GetOwner(item.PROJ_LOT_OWNER);
                pLotOwner = account.OwnerName;
            }
        }

        private void GetScopeItem(string sScopeCode)
        {
            string sScopeDesc = string.Empty;
            ScopeList scope = new ScopeList();
            sScopeDesc = scope.GetScopeDesc(sScopeCode);

            if (sScopeDesc == "NEW")
                pScopeNew = "x";
            else if (sScopeDesc == "ADDITION")
                pScopeAdd = "x";
            else if (sScopeDesc == "REPAIR")
                pScopeRepair = "x";
            else if (sScopeDesc == "RENOVATION")
                pScopeReno = "x";
            else if (sScopeDesc == "ERECTION")
                pScopeErection = "x";
            else if (sScopeDesc == "MOVING")
                pScopeMove = "x";
            else if (sScopeDesc == "RAISING")
                pScopeRaise = "x";
            else if (sScopeDesc == "ACCESSORY BLDG/ STRUC")
                pScopeAcce = "x";
            else if (sScopeDesc == "CONVERSION")
                pScopeConvert = "x";
            else if (sScopeDesc == "ALTERATION")
                pScopeAlter = "x";
            else
                pScopeOthers = "x";
        }

        private void GetBuilding(int iBldgNo)
        {
            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            var result = from a in Records.Building.GetBuildingRecord(iBldgNo)
                         select a;

            foreach (var item in result)
            {
                
                pFlrArea = item.TOTAL_FLR_AREA.ToString("#,###.00");
                pNoUnits = item.NO_UNITS.ToString("#,###");
                pEstCost = item.EST_COST.ToString("#,###.00");
                
            }
        }
    }
}
