using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class ObjectModels
    {
        public class DocObject
        {
            public string doc_add_1 { get; set; }
            public string doc_nickname { get; set; }
            public bool doc_verification { get; set; }
            public string doc_add_2 { get; set; }
            public int doc_password_time { get; set; }
            public string doc_sign { get; set; }
            public string doc_gender { get; set; }
            public int doc_forbid_time { get; set; }
            public string doc_email { get; set; }
            public int doc_active_time { get; set; }
            public string doc_agreement { get; set; }
            public string doc_name { get; set; }
            public string user_phone { get; set; }
            public string doc_level { get; set; }
            public string doc_hospital { get; set; }
            public string doc_birth { get; set; }
            public string doc_account_status { get; set; }
            public string doc_license_tcm_url { get; set; }
            public string doc_identity { get; set; }
            public bool doc_cert { get; set; }
            public string[] doc_department { get; set; }
            public string doc_id { get; set; }
            public string doc_license_url { get; set; }
            public string doc_photo { get; set; }
            public string doc_location { get; set; }
            public string doc_level_option { get; set; }
            public int doc_date { get; set; }
            public string doc_status { get; set; }
            public string _id { get; set; }
            public string doc_identity_option { get; set; }
            public Doc_Syndromes[] doc_syndromes { get; set; }
            public string doc_unionid { get; set; }
            public Doc_Models[] doc_models { get; set; }
        }

        public class Doc_Syndromes
        {
            public string[] value { get; set; }
            public string key { get; set; }
        }

        public class Doc_Models
        {
            public int[] date { get; set; }
            public string model { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Error
        {
            public string message { get; set; }
        }


        public class FormulaObject
        {
            public string formula_name { get; set; }
            public int create_time { get; set; }
            public bool formula_delete { get; set; }
            public string taboo { get; set; }
            public string[][] formula_ingredient { get; set; }
            public string usage { get; set; }
            public string doc_id { get; set; }
            public string[] formula_treat { get; set; }
            public string follow_up { get; set; }
            public string pathogenesis { get; set; }
            public int last_edit_time { get; set; }
            public bool formula_use { get; set; }
            public string effect { get; set; }
            public int formula_delete_time { get; set; }
            public string[] formula_type { get; set; }
            public string _id { get; set; }
            public string side_effect { get; set; }
        }

        public class OB
        {
            public string id;
            public string formula_treat;
            public int count;
            public string order;
        }

        public class DataObject
        {

            public string
                  data;
        }


        public class MemberObject
        {
            public string user_phone { get; set; }
            public string user_code { get; set; }
        }


        public class PasswordChangeObject
        {
            public string password { get; set; }
            public string new_password { get; set; }
            public string user_phone { get; set; }
        }

        public class PasswordForgetObject
        {
            public string phone { get; set; }
            public string code { get; set; }
            public string new_password { get; set; }
        }

        public class OrderObject
        {
            public Doctor[] doctor { get; set; }
            public string order_status { get; set; }
            public string wx_appid { get; set; }
            public int order_create_time { get; set; }
            public int completion_time { get; set; }
            public int order_paid_time { get; set; }
            public string wx_mch_id { get; set; }
            public string order_amount { get; set; }
            public int doc_accept_time { get; set; }
            public int order_id { get; set; }
            public Doc_Sign[] doc_sign { get; set; }
        }

        public class Doctor
        {
            public string doc_name { get; set; }
            public string doc_id { get; set; }
        }

        public class Doc_Sign
        {
            public string sign_pic { get; set; }
            public string doc_id { get; set; }
        }

        public class OrderPostObject
        {
            public Data data { get; set; }
            public string message { get; set; }
        }

        public class Data
        {
            public Doctor[] doctor { get; set; }
            public string order_status { get; set; }
            public string wx_appid { get; set; }
            public int order_create_time { get; set; }
            public int completion_time { get; set; }
            public int order_paid_time { get; set; }
            public string wx_mch_id { get; set; }
            public string order_amount { get; set; }
            public int doc_accept_time { get; set; }
            public int order_id { get; set; }
            public Doc_Sign[] doc_sign { get; set; }
        }


        public class PrescriptionObject
        {
            public string[] px_neg_disease { get; set; }
            public string[] formula_ii_description { get; set; }
            public string user_name { get; set; }
            public string[] pulse_right_tags { get; set; }
            public string[] tongue_tags { get; set; }
            public string user_gender { get; set; }
            public string doc_sign { get; set; }
            public string formula_auditors { get; set; }
            public string doctor_id { get; set; }
            public int formula_ii_dosage_per_day { get; set; }
            public int px_start_time { get; set; }
            public int[] px_score { get; set; }
            public string[] pulse_left_tags { get; set; }
            public string user_date { get; set; }
            public string user_phone { get; set; }
            public string pulse_right_id { get; set; }
            public string doctor_name { get; set; }
            public int pulse_left_time { get; set; }
            public string px_type { get; set; }
            public Tongue_Details tongue_details { get; set; }
            public string[][] formula_ingredient { get; set; }
            public string[] model_type { get; set; }
            public string disease_diagnosis { get; set; }
            public string[] px_pos_disease { get; set; }
            public string formula_ii_name { get; set; }
            public string tongue_pic_url { get; set; }
            public int px_end_time { get; set; }
            public int pulse_right_time { get; set; }
            public string tongue_pic_base_url { get; set; }
            public string user_id { get; set; }
            public string formula_discription { get; set; }
            public string pulse_left_id { get; set; }
            public int formula_ii_time_per_dosage { get; set; }
            public string formula_type { get; set; }
            public string _id { get; set; }
            public string formula_pharmacist { get; set; }
            public string order_id { get; set; }
            public string[] formula_order { get; set; }
            public string[] px_igg_disease { get; set; }
            public int formula_ii_total_dosage { get; set; }
        }

        public class Tongue_Details
        {
            public Tongue_Coating_Color tongue_coating_color { get; set; }
            public string[] pic_tongue_quality { get; set; }
            public string[] pic_tongue_describe { get; set; }
            public string[] pic_tongue_disease_proof { get; set; }
            public string[] pic_tongue_probably_prescription { get; set; }
            public string[] affect_areas_tongueBottom { get; set; }
            public string[] pic_tongue_status { get; set; }
            public Tongue_Color tongue_color { get; set; }
            public string[] pic_tongue_disease { get; set; }
            public Tongue_Coating_Status tongue_coating_status { get; set; }
            public string[] pic_tongue_disease_status { get; set; }
        }

        public class Tongue_Coating_Color
        {
            public string[] side { get; set; }
            public string[] middle { get; set; }
            public string[] root { get; set; }
            public string[] whole { get; set; }
            public string[] tip { get; set; }
        }

        public class Tongue_Color
        {
            public string[] side { get; set; }
            public string[] middle { get; set; }
            public string[] root { get; set; }
            public string[] whole { get; set; }
            public string[] tip { get; set; }
        }

        public class Tongue_Coating_Status
        {
            public string[] side { get; set; }
            public string[] middle { get; set; }
            public string[] root { get; set; }
            public string[] whole { get; set; }
            public string[] tip { get; set; }
        }


        public class UserObject
        {
            public string user_email { get; set; }
            public string user_openid { get; set; }
            public string user_account_status { get; set; }
            public string user_active_time { get; set; }
            public string user_name { get; set; }
            public int user_weight { get; set; }
            public string user_agreement { get; set; }
            public string user_suspension_time { get; set; }
            public string user_gender { get; set; }
            public int user_height { get; set; }
            public string user_add_2 { get; set; }
            public string user_unionid { get; set; }
            public int user_birthday { get; set; }
            public string user_add_1 { get; set; }
            public string user_id { get; set; }
            public string user_chpasswd_time { get; set; }
            public int user_date { get; set; }
            public string user_nickname { get; set; }
            public string user_phone { get; set; }
            public string _id { get; set; }
            public User_Location user_location { get; set; }
            public string user_photo { get; set; }
        }

        public class User_Location
        {
            public float lng { get; set; }
            public float lat { get; set; }
        }


        public class UserModelsObject
        {

            public string authToken;
            public List<string> modelFlags;
        }
        public class UserQaObject
        {

            public string authToken;
            public List<string> ignored;
            public List<string> posAnswer;
            public List<string> negAnswer;
            public List<string> wideMode;
            public int horizontalLimit;
            public int verticalLimit;
            public float inputLength;
            public int bindingStartPoint;
            public int bindingLength;
        }


        public class UserQaResponseObject
        {
            public string authToken { get; set; }
            public int totalQaNumber { get; set; }
            public string qaHash { get; set; }
            public Result[] results { get; set; }
        }

        public class Result
        {
            public Target[] targets { get; set; }
            public string name { get; set; }
            public object rating { get; set; }
        }

        public class Target
        {
            public string name { get; set; }
            public int rating { get; set; }
        }
        public class UserSpecialsObject
        {

            public string authToken;
        }
        public class UserStatusResponseObject
        {
            public string[] posAnswer { get; set; }
            public string[] negAnswer { get; set; }
            public string[] ignored { get; set; }
        }
        public class UserStatusUpdateObject : UserSpecialsObject
        {

            public string[] posAnswer { get; set; }
            public string[] negAnswer { get; set; }

        }
        public class UserDiagnosisObject : UserSpecialsObject
        {

            public int limit { get; set; }

        }
        public class UserQueryObject : UserSpecialsObject
        {

            public int limit { get; set; }
            public List<string> authors { get; set; }

        }





        public class UserDiagnosisResponseObject
        {
            public int limit { get; set; }
            public UserDiagnosisResponseObjectResult[] results { get; set; }
        }

        public class UserDiagnosisResponseObjectResult
        {
            public int rating { get; set; }
            public UserDiagnosisResponseObjectTarget[] targets { get; set; }
            public float confidence { get; set; }
            public object validity { get; set; }
            public object precision { get; set; }
            public object recall { get; set; }
            public string doctor { get; set; }
            public object extras { get; set; }
        }

        public class UserDiagnosisResponseObjectTarget
        {
            public string[] targets { get; set; }
            public float confidence { get; set; }
            public object validity { get; set; }
            public object precision { get; set; }
            public object recall { get; set; }
            public string[] solved { get; set; }
            public string[] wronged { get; set; }
            public string[] taboo { get; set; }
            public Herb[] herbs { get; set; }
        }

        public class Herb
        {
            public string name { get; set; }
            public float amount { get; set; }
            public int amountStandard { get; set; }
            public string unit { get; set; }
            public object unitStandard { get; set; }
            public object function { get; set; }
        }


        public class GetVersionObject
        {
            public string version { get; set; }
            public string[] modelFlags { get; set; }
        }

    }
}