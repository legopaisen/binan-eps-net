using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AppSettings
{
    /// <summary>
    /// This is just an entity bean of commonly used configuration settings
    /// </summary>
    public static class ConfigurationAttributes
    {
        public static string ChiefTaxDivision
        {
            get { return AppSettingsManager.GetConfigValue("115").Trim(); }
        }
        // GDE 20090209 for LGU Code (s){
        public static string LGUCode
        {
            get { return AppSettingsManager.GetConfigObject("04").Trim(); }
        }
        // GDE 20090209 for LGU Code (e)}
        // GDE 20090209 for Dist Code (s){
        public static string DistCode
        {
            get { return AppSettingsManager.GetConfigObject("11").Trim(); }
        }
        // GDE 20090209 for Dist Code (e)}

        // GDE 20090220 for LGU Name(s){
        //public static string LGUName
        //{
         //   get { return AppSettingsManager.GetConfigObject("01").Trim(); }
        //}
        // GDE 20090220 for LGU Name(e)}

        public static string LguName2
        {
            get
            {
                string strLguName = string.Empty;

                if (AppSettingsManager.GetConfigValueByDescription("LGU TYPE").Trim() == "CITY")
                    strLguName = "CITY OF " + AppSettingsManager.GetConfigDescription("02").Trim();
                else if (AppSettingsManager.GetConfigValueByDescription("LGU TYPE").Trim() == "PROVINCIAL")
                    strLguName = "PROVINCE OF " + AppSettingsManager.GetConfigDescription("02").Trim();
                else if (AppSettingsManager.GetConfigValueByDescription("LGU TYPE").Trim() == "MUNICIPALITY")
                    strLguName = "MUNICIPALITY OF " + AppSettingsManager.GetConfigDescription("02").Trim();

                return strLguName;
            }
        }

        public static string LGUName
        {
            get { return AppSettingsManager.GetConfigDescription("02").Trim(); }
        }

        public static string RevYear
        {
            get { return AppSettingsManager.GetConfigObject("07").Trim(); } // ALJ 20090703 REVISION YEAR
        }

        public static string CurrentYear
        {
            get { return AppSettingsManager.GetConfigObject("12").Trim(); } // ALJ 20100217 CURRENT YEAR
        }

        public static string AutoApplication
        {
            get { return AppSettingsManager.GetConfigObject("27").Trim(); } // ALJ 20100218 AUTO-APPLICATION
        }

        public static bool HasAdjustmentFeature
        {
            get { return AppSettingsManager.GetConfigValueByDescription("HAS ADJUSTMENT FEATURE") == "Y"; }
        }

        public static bool HasAdjustments
        {
            get { return AppSettingsManager.GetConfigValueByDescription("APPLY ADJUSTMENT").Trim() == "Y"; }
        }

        //public static string LGUName
        //{
        //    get { return AppSettingsManager.GetConfigValueByDescription("LGU NAME"); }
        //}

        public static string ProvinceName
        {
            get { 
                    if(AppSettingsManager.GetConfigValueByDescription("PROVINCE").Contains("PROVINCE"))
                        return AppSettingsManager.GetConfigValueByDescription("PROVINCE");
                    else
                        return "PROVINCE OF " + AppSettingsManager.GetConfigValueByDescription("PROVINCE");
            }
        }

        public static string Version
        {
            get { return AppSettingsManager.GetConfigValue("60"); }
        }

        public static bool HasAttributeEnableAssessorApprovalModule
        {
            get { return AppSettingsManager.GetConfigValue("117") == "Y"; }
        }

        public static string AssessorOfficeName
        {
            get
            {
                string strVersion = ConfigurationAttributes.Version;
                string strAssessorOfficeName = string.Empty;

                if (strVersion == "P")
                    strAssessorOfficeName = "Provincial";
                else if (strVersion == "CC" || strVersion == "C")
                    strAssessorOfficeName = "City";
                else if (strVersion == "PM" || strVersion == "P")
                    strAssessorOfficeName = "Municipality";

                return string.Format("{0} Assessor's Office", strAssessorOfficeName);
            }
        }

        //RDO 021508 (s) flag that enables program features
        public static bool IsShowExtendedPins
        {
            //get { return true; }
            get
            {
                //RDO 031208 (s) get from configuration
                if (AppSettingsManager.GetConfigValueByDescription("SHOW EXTENDED PINS").Trim() == "Y")
                    return true;
                //RDO 031208 (e) get from configuration
                return false;
            }
        }


        //JVL (s)
        /// <summary>
        /// it only disable / enable the display but the actual comutation and saving of 
        /// JVLarion but has conflict if LGU has BOTH fire tax and idle land tax is enable it need to customized
        /// </summary>
        public static bool HasFireTax
        {
            get
            {
                if (AppSettingsManager.GetConfigValueByDescription("HAS FIRE TAX").Trim() == "Y")
                    return true;
                return false;
            }
        }

        //JVL (s)
        /// <summary>
        /// it only disable / enable the display but the actual comutation and saving of 
        /// JVLarion but has conflict if LGU has BOTH fire tax and idle land tax is enable it need to customized
        /// </summary>
        public static bool HasIdleLandTax
        {
            get
            {
                if (AppSettingsManager.GetConfigValueByDescription("HAS IDLE LAND TAX").Trim() == "Y")
                    return true;
                return false;
            }
        }


        //JVL (s)

        /*
        public static bool IsAllowMultipleOR
        {
            get
            {
                if (AppSettingsManager.IsDebugMode)
                    return true;
                return false;
            }
        }
        //RDO 021508 (e) flag that enables program features
        */

        //RDO 0707208 (s) actually returns the opposite value
        public static bool IsManualDeclaration
        {
            get
            {
                bool blnManualDeclaration = false;
                string strConfigValue = AppSettingsManager.GetConfigValueByDescription("MANUAL DECLARATION OF OR").Trim();
                if (strConfigValue == "N")
                    blnManualDeclaration = true;

                return blnManualDeclaration;
            }
        }

        public static string TreasurerName
        {
            get
            {
                string strVersion = AppSettingsManager.GetConfigValue("60");
                string strTreasurerName = string.Empty;
                if (strVersion == "P" || strVersion == "CC" || strVersion == "C")
                    strTreasurerName = AppSettingsManager.GetConfigValue("08");
                else if (strVersion == "PM")
                    strTreasurerName = AppSettingsManager.GetConfigValue("97");
                else
                    strTreasurerName = AppSettingsManager.GetConfigValueByDescription("TREASURER");

                return strTreasurerName;
            }
        }

        public static string TreasurerPositionName
        {
            get
            {
                string strVersion = AppSettingsManager.GetConfigValue("60");
                string strTreasurerNamePosition = string.Empty;
                if (strVersion == "P")
                    strTreasurerNamePosition = "Provincial Treasurer";
                else if (strVersion == "PM")
                    strTreasurerNamePosition = "Municipal Treasurer";
                else if (strVersion == "CC" || strVersion == "C")
                    strTreasurerNamePosition = "City Treasurer";

                return strTreasurerNamePosition;
            }
        }

        public static string OfficeName
        {
            get
            {
                string strVersion = AppSettingsManager.GetConfigValue("60").Trim();
                string strOfficeName = string.Empty;

                if (AppSettingsManager.GetConfigValueByDescription("IS CITY").Trim() == "Y")
                    strOfficeName = "Office of the City Treasurer";
                //AppSettingsManager.GetConfigValue("60").Trim() == "P")
                else if (strVersion == "P") // || strVersion == "PM")
                    strOfficeName = "Office Of The Provincial Treasurer";
                else
                    strOfficeName = "Office Of The Municipal Treasurer";

                return strOfficeName;
            }
        }

        /*
        public static string JurisdictionName
        {
            get
            {
                string strVersion = string.Empty;
                string strJurisdictionName = string.Empty;
                strVersion = AppSettingsManager.GetConfigValue("60");
                if (strVersion == "P")
                    strJurisdictionName = "Provincial";
                else if (strVersion == "CC" || strVersion == "C")
                    strJurisdictionName = "City";
                else if (strVersion == "PM" || strVersion == "P")
                    strJurisdictionName = "Municipality";

                return strJurisdictionName;
            }
        }
        */


    }
}
