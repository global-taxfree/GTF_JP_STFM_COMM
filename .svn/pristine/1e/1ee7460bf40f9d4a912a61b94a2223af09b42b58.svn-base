using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTF_Comm;
using GTF_STFM_COMM.Util;
using Newtonsoft.Json.Linq;
namespace GTF_STFM_COMM.Tran
{
    //거래실행. 필요시 로컬 DB 정보 업데이트
    class Transaction
    {
        //public string ServerUrl     { get; set; }
        public string ServerIp      { get; set; }
        public string ServerPort    { get; set; }

        string url_Login = string.Empty;    //로그인
        string url_Info = string.Empty;     //발행관련정보
        string url_Slip = string.Empty;  //거래등록
        string url_Closing = string.Empty;  //마감
        string url_Closing_cancel = string.Empty;  //마감취소
        string url_Status_check = string.Empty;  //온라인마감정보 확인
        string url_Sign = string.Empty;  //Sign 등록
        string url_ServerSlip = string.Empty; //온라인 거래조회
        string url_ServerSlipCount = string.Empty; //온라인 거래조회 건수
        string url_ServerSlipDetail = string.Empty; //온라인 거래 상세조회
        string url_ServerSlipGoodsDetail = string.Empty; //온라인 거래 물품상세조회
        string url_CancelAll = string.Empty; //온라인 전체 거래 취소
        string url_Cancel = string.Empty; //온라인 거래 취소
        string url_PrintSlipInfo = string.Empty; //온라인 거래 재발행

        public Transaction()
        {
            url_Login = "/service/jtc/offLogin";     //로그인
            url_Info = "/service/jtc/getOffInfo";    //발행관련정보
            url_Slip = "/service/jtc/addOffSlipIssue";             //거래등록
            url_Closing = "/service/jtc/offClosing"; //마감
            url_Closing_cancel = "/service/jtc/offClosingCancel"; //마감
            url_Sign = "/service/jtc/addSlipSign";
            url_ServerSlip = "/service/jtc/onlineSearch"; //온라인 거래조회
            url_ServerSlipCount = "/service/jtc/onlineSearchCount"; //온라인 거래조회 건수
            url_ServerSlipDetail = "/service/jtc/onlineSearchDetail"; //온라인 거래 상세조회
            url_ServerSlipGoodsDetail = "/service/jtc/onlineSearchGoodsDetail"; //온라인 물품 상세조회
            url_CancelAll = "/service/jtc/cancelSlipAll"; //온라인 물품 상세조회
            url_Cancel = "/service/jtc/cancelSlip"; //온라인 물품 상세조회
            url_PrintSlipInfo = "/service/jtc/printSlipServerInfo"; //온라인 물품 상세조회
            IgnoreBadCertificates();
        }
           
        //서버 전표 등록 최종 확인
        public JObject Login(String strUserId , String strPassword)
        {
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();
                //JArray arrJsonRes = null;
                //JObject jsonRes = null;

                jsonReq.Add("userId", strUserId);
                jsonReq.Add("passWord", strPassword);
                //jsonReq.Add("openDate", DateTime.Now.ToString("yyyyMMdd")); //오늘날짜
                //jsonReq.Add("openDate", "20170725");
                //jsonReq["openDate"] = "20170726";
                jsonReq["openDate"] = DateTime.Now.ToString("yyyyMMdd"); //오늘날짜

                Constants.OPEN_DATE = jsonReq["openDate"].ToString();    //오늘날짜 저장
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("companyID", "000001");

                strReq = jsonReq.ToString();
                //ServerUrl = Constants.SERVER_URL+ url_Login;
                //Constants.LOGGER_MAIN.Error(Constants.SERVER_URL + url_Login);
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Login, strReq, true);
                

                if (strRes == null || string.Empty.Equals(strRes.Trim()) )
                {
                    //처리오류
                    Constants.OPEN_DATE = string.Empty;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);

                    if ("S".Equals(jsonRes["login"].ToString()))
                    {
                        //로그인성공
                        //String strTempUserInfo = jsonRes["user_info"].ToString();
                        JObject user_info = jsonRes["userInfo"].Value<JObject>();
                        IList<string> keys = user_info.Properties().Select(p => p.Name).ToList();
                        
                        Constants.LDB_MANAGER.DropTable("USERINFO".ToUpper()); //DB 삭제
                        Constants.LDB_MANAGER.createTable(user_info , "USERINFO".ToUpper());//DB생성
                        Constants.LDB_MANAGER.InsertTable(user_info, "USERINFO".ToUpper());
                        //Constants.SQLITE_MANAGER.InsertTable(user_info, "USERINFO".ToUpper(), 2);
                        strRes = "OK";
                    }
                    else
                    {
                        //로그인실패
                        Constants.OPEN_DATE = string.Empty;
                    }
                }
            }
            catch(Exception e)
            {
                Constants.LOGGER_MAIN.Error(e.Message, e);
                Constants.OPEN_DATE = string.Empty;
            }
            return jsonRes;
        }

        //업무마감
        public Boolean Closing(String strUserId, string OpenDate  , string saleCnt, string saleAmt, string taxAmt, string feeAmt)
        {
            Boolean bRet = true;
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();
                //JArray arrJsonRes = null;
                //JObject jsonRes = null;
                jsonReq.Add("userId", strUserId);
                //jsonReq.Add("openDate", DateTime.Now.ToString("yyyyMMdd")); //오늘날짜
                jsonReq.Add("openDate", Constants.OPEN_DATE); //오늘날짜
                //jsonReq.Add("openDate", "20170615");
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("companyID", "000001");

                jsonReq.Add("saleCnt", saleCnt);
                jsonReq.Add("saleAmt", saleAmt);
                jsonReq.Add("taxAmt", taxAmt);
                jsonReq.Add("feeAmt", feeAmt);

                strReq = jsonReq.ToString();
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Closing, strReq, true);

                if (strRes == null || string.Empty.Equals(strRes.Trim()))
                {
                    //처리오류
                    bRet = false;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);
                    if ("S".Equals((string)jsonRes["result"]))
                    {
                        bRet = true;
                    }
                    else
                    {
                        //로그인실패
                        bRet = false;
                    }
                }
            }
            catch (Exception e)
            {
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return bRet;
        }


        // 온라인 전체 취소
        public Boolean CancelAll(String strUserId, string slip_no, string totalSlipSeq)
        {
            Boolean bRet = true;
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();
                
                jsonReq.Add("userID", strUserId);
                jsonReq.Add("slip_no", slip_no); //오늘날짜
                jsonReq.Add("totalSlipSeq", totalSlipSeq);

                strReq = jsonReq.ToString();
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_CancelAll, strReq, true);

                if (strRes == null || string.Empty.Equals(strRes.Trim()))
                {
                    //처리오류
                    bRet = false;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);
                    if ("S".Equals((string)jsonRes["result"]))
                    {
                        bRet = true;
                    }
                    else
                    {
                        //로그인실패
                        bRet = false;
                    }
                }
            }
            catch (Exception e)
            {
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return bRet;
        }

        // 온라인 부분 취소
        public Boolean Cancel(String strUserId, string slip_no, string totalSlipSeq)
        {
            Boolean bRet = true;
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();

                jsonReq.Add("userID", strUserId);
                jsonReq.Add("slip_no", slip_no); //오늘날짜
                jsonReq.Add("totalSlipSeq", totalSlipSeq);

                strReq = jsonReq.ToString();
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Cancel, strReq, true);

                if (strRes == null || string.Empty.Equals(strRes.Trim()))
                {
                    //처리오류
                    bRet = false;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);
                    if ("S".Equals((string)jsonRes["result"]))
                    {
                        bRet = true;
                    }
                    else
                    {
                        //로그인실패
                        bRet = false;
                    }
                }
            }
            catch (Exception e)
            {
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return bRet;
        }

        //업무마감 취소 
        public Boolean ClosingCancel(String strUserId, string OpenDate)
        {
            Boolean bRet = true;
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();
                jsonReq.Add("userId", strUserId);
                jsonReq.Add("openDate", Constants.OPEN_DATE); //오늘날짜
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("companyID", "000001");

                strReq = jsonReq.ToString();
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Closing_cancel, strReq, true);

                if (strRes == null || string.Empty.Equals(strRes.Trim()))
                {
                    //처리오류
                    bRet = false;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);
                    if ("S".Equals((string)jsonRes["result"]))
                    {
                        //마감취소성공
                        bRet = true;
                    }
                    else
                    {
                        //마감취소실패
                        bRet = false;
                    }
                }
            }
            catch (Exception e)
            {
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return bRet;
        }

        public JArray SearchUserInfo(string strUserId, string strMerchantNo, string strDeskId, string strCode_type)
        {
            JObject jsonReq = new JObject();
            JObject jsonRes = null;
            JArray arrJsonRes = null;

            jsonReq.Add("userId", strUserId);
            jsonReq.Add("merchantNo", strMerchantNo);
            jsonReq.Add("deskId", strDeskId);
            //jsonReq.Add("languageCD", strLanguageCD);
            //jsonReq.Add("companyID", strCompanyID );
            jsonReq.Add("languageCD", "jp");
            jsonReq.Add("companyID", "000001");
            jsonReq.Add("code_type", strCode_type);

            GTF_CommManager comm = new GTF_CommManager(null);
            string strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Info, jsonReq.ToString(), true);
            if (strRes == null || string.Empty.Equals(strRes.Trim()))
            {
                //처리오류
            }
            else
            {
                strRes = strRes.Replace("`", "'"); //UPI BIN 에서 ` 문자가 오는 경우가 있다.
                //strRes = strRes.Replace("REG_DTM\":", "REG_DTM\":\""); //REG_DTM 의 데이터가 " 없이 온다.
                //strRes = strRes.Replace(",\"MOD_DTM", "\",\"MOD_DTM"); //REG_DTM 의 데이터가 " 없이 온다.
                jsonRes = JObject.Parse(strRes);

                if ("S".Equals((string)jsonRes["result"]))
                {
                    //조회 성공
                    //String strTempUserInfo = jsonRes["user_info"].ToString();
                    if (strCode_type.Equals("ads"))
                    {
                        arrJsonRes = jsonRes["adsImageList"].Value<JArray>();
                    }
                    else
                    {
                        arrJsonRes = jsonRes[strCode_type + "List"].Value<JArray>();
                    }

                    strRes = "OK";
                    for (int i = 0; i < arrJsonRes.Count; i++)
                    {
                        JObject tmpObj = (JObject)arrJsonRes[i];
                        int insert_Flag = 2;

                        //if (jsonWhere == null ) 
                        {
                            if (arrJsonRes.Count == 1) insert_Flag = 2;
                            else
                            {
                                if (i == 0)
                                {
                                    insert_Flag = 0; string strDate = DateTime.Now.ToString("yyyyMMdd");
                                    string strTime = DateTime.Now.ToString("hhmmss"); Constants.LOGGER_MAIN.Info("START22::" + strDate + " " + strTime);
                                }
                                else if (i == (arrJsonRes.Count - 1))
                                {
                                    insert_Flag = 1; string strDate = DateTime.Now.ToString("yyyyMMdd");
                                    string strTime = DateTime.Now.ToString("hhmmss"); Constants.LOGGER_MAIN.Info("END22" + strDate + " " + strTime);
                                }
                            }

                            //if(!Constants.LDB_MANAGER.updateTable(tmpObj, strTableKey, strCode_type.ToUpper()))
                            if ("code".Equals(strCode_type)&& Constants.LDB_MANAGER.ExitsTable("CODE"))
                            {
                                JObject jsonWhere = new JObject();
                                jsonWhere.Add("CODEDIV", tmpObj["CODEDIV"].ToString());
                                jsonWhere.Add("COMCODE", tmpObj["COMCODE"].ToString());
                                JArray arrRet = Constants.LDB_MANAGER.SelectTable(strCode_type.ToUpper(), jsonWhere);
                                if (arrRet != null && arrRet.Count > 0)
                                {
                                    Constants.LDB_MANAGER.updateTable(tmpObj, jsonWhere, strCode_type.ToUpper());
                                }
                                else
                                {
                                    //Constants.SQLITE_MANAGER.InsertTable(tmpObj, strCode_type.ToUpper(), insert_Flag);
                                    Constants.LDB_MANAGER.InsertTable(tmpObj, strCode_type.ToUpper());
                                }
                                if (arrRet != null)
                                    arrRet.RemoveAll();
                                if (jsonWhere != null)
                                    jsonWhere.RemoveAll();
                                arrRet = null;
                                jsonWhere = null;
                            }
                            else {
                                if (i == 0)
                                {
                                    Constants.LDB_MANAGER.DropTable(strCode_type.ToUpper()); //DB 삭제
                                    Constants.LDB_MANAGER.createTable(tmpObj, strCode_type.ToUpper());//DB생성
                                }
                                //if (!Constants.LDB_MANAGER.updateTable(tmpObj, jsonWhere, strCode_type.ToUpper()))
                                //Constants.SQLITE_MANAGER.InsertTable(tmpObj, strCode_type.ToUpper(), insert_Flag);
                                Constants.LDB_MANAGER.InsertTable(tmpObj, strCode_type.ToUpper());
                            }
                        }
                        //else
                        //{
                        //    JArray tempArr = Constants.LDB_MANAGER.SelectTable(strCode_type.ToUpper(), jsonWhere);
                        //    if (tempArr == null || tempArr.Count == 0)
                        //    {
                        //        Constants.LDB_MANAGER.InsertTable(tmpObj, strCode_type.ToUpper());
                        //    }
                        //    else
                        //    {
                        //        Constants.LDB_MANAGER.updateTable(tmpObj , jsonWhere, strCode_type.ToUpper());
                        //    }
                        //}

                    }
                }
                else
                {
                    //조회 실패
                }
            }
            return arrJsonRes;
        }

        public string getSlipPrintInfo(string slip_no)
        {
            JObject jsonReq = new JObject();

            jsonReq.Add("slip_no", slip_no); //오늘날짜

            GTF_CommManager comm = new GTF_CommManager(null);
            string strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_PrintSlipInfo, jsonReq.ToString(), true);

            return strRes;
        }


        public string UserInfo(string strUserId)
        {
            string strRes = string.Empty;
            return strRes;
        }


        //서버 전표 등록
        public Boolean Submit(string strData)
        {
            string strRes = string.Empty;
            Boolean bRet = false;
            GTF_CommManager comm = new GTF_CommManager(null);
            strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Slip, strData, true);
            if (strRes != null && !string.Empty.Equals(strRes.Trim()))
            {
                JObject jsonRes = JObject.Parse(strRes);

                if ("S".Equals((string)jsonRes["result"]))
                {
                    bRet = true;
                }
                else if ("D".Equals((string)jsonRes["result"]))
                {
                    bRet = true;
                }
                else if ("F".Equals((string)jsonRes["result"]))
                {
                    bRet = true;
                }
            }
            return bRet;
        }

        //서버 전표 등록 최종 확인
        public string Confirm(JObject jsonReq)
        {
            string strRes = string.Empty;
            return strRes;

        }

        

        //매장목록 조회
        public JArray SearchShopList(JObject jsonWhere = null)
        {
            //로컬DB 조회
            JArray arrjsonRes = new JArray();
            arrjsonRes = Constants.LDB_MANAGER.SelectTable("MERCHANT", jsonWhere);
            return arrjsonRes;
        }

        public JArray SearchAdsList(JObject jsonWhere = null)
        {
            //로컬DB 조회
            string strReq = string.Empty;
            string strRes = string.Empty;
            JArray arrjsonRes = new JArray();
            //DB 조회
            arrjsonRes = Constants.LDB_MANAGER.SelectTable("ADS", jsonWhere);
            return arrjsonRes;
        }


        public JArray SearchContryList(JObject jsonWhere = null)
        {
            //로컬DB 조회
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonReq = new JObject();
            JObject jsonTempRes = null;
            JArray arrjsonTempRes = null;
            JArray arrjsonRes = new JArray();
            int seq_num = 0;

            JArray arrjsonBefore = new JArray();
            Utils utils = new Utils();
            ArrayList beoreList = utils.GetFixOrderCountryList();

            //DB 조회
            arrjsonTempRes = Constants.LDB_MANAGER.SelectTableOrderCounry("CODE", jsonWhere);
            
            if (arrjsonTempRes != null && arrjsonTempRes.Count > 0)
            {
                for (int i = 0; i < arrjsonTempRes.Count; i++)
                {
                    //국적만 받아옴
                    jsonTempRes = new JObject();
                    jsonTempRes.Add(((JObject)arrjsonTempRes[i])["COMCODE"].ToString(), ((JObject)arrjsonTempRes[i])["ATTRIB02"].ToString());
                    
                    if (beoreList.Contains(((JObject)arrjsonTempRes[i])["COMCODE"].ToString()))
                    {
                        jsonTempRes.Add("SEQ", (beoreList.IndexOf(((JObject)arrjsonTempRes[i])["COMCODE"].ToString()) - beoreList.Count));
                    }
                    else
                    {
                        jsonTempRes.Add("SEQ", seq_num );
                        seq_num++;
                    }
                    arrjsonRes.Add(jsonTempRes);
                }

                JArray sorted = new JArray(arrjsonRes.OrderBy(obj => obj["SEQ"]));
                for (int i = 0; i < sorted.Count; i++)
                {
                    jsonTempRes = (JObject)sorted[i];
                    jsonTempRes.Remove("SEQ");
                }
                arrjsonRes = sorted;
            }
            utils = null;
            beoreList.Clear();
            return arrjsonRes;
        }

        //콤보 아이템 목록 갱신
        public JArray SearchItemList(JObject jsonWhere = null)
        {
            //로컬DB 조회
            JObject jsonReq = new JObject();
            JArray arrjsonRes = new JArray();
            JObject jsonTempRes = null;
            JArray arrjsonTempRes = null;
            //로컬 DB 조회
            arrjsonTempRes = Constants.LDB_MANAGER.SelectTable("ITEM", jsonWhere);
            for (int i = 0; i < arrjsonTempRes.Count; i++)
            {
                //국적만 받아옴
                jsonTempRes = (JObject)arrjsonTempRes[i];
                string tempData = jsonTempRes["SEQ"].ToString();
                jsonTempRes.Remove("SEQ");
                jsonTempRes.Add("SEQ", Int32.Parse(tempData));
                arrjsonRes.Add(jsonTempRes);
            }
            JArray sorted = new JArray(arrjsonRes.OrderBy(obj => obj["SEQ"]));
            arrjsonRes = sorted;
            return arrjsonRes;
        }


        public long getSeq(string strKey)
        {
            string strRet = string.Empty;
            long nSeq = 0;
            JObject jsonWhere = new JObject();
            JObject jsonInsert = new JObject();
            JObject jsonUpdate = new JObject();
            jsonWhere.Add("ITEM_KEY", strKey);

            JArray arrRet = Constants.LDB_MANAGER.SelectTable("ID_CREATION_RULE", jsonWhere);
            if (arrRet == null || arrRet.Count == 0)
            {
                jsonInsert.Add("ITEM_KEY", strKey);
                jsonInsert.Add("ITEM_DESC", strKey);
                jsonInsert.Add("ITEM_VALUE", "0");
                //Constants.SQLITE_MANAGER.InsertTable(jsonInsert, "ID_CREATION_RULE", 2);
                Constants.LDB_MANAGER.InsertTable(jsonInsert, "ID_CREATION_RULE");
                jsonInsert.RemoveAll();
                jsonInsert = null;
            }
            else
            {
                JObject jsonRet = (JObject)arrRet[0];
                nSeq = Int64.Parse(jsonRet["ITEM_VALUE"].ToString());
                if (nSeq == Int64.MaxValue)//최대숫자에 도달하면 0으로 변경. 해당경우는 없겠지만...
                    nSeq = 0;
                else
                    nSeq++;
                jsonUpdate.Add("ITEM_VALUE", nSeq.ToString());

                Constants.LDB_MANAGER.updateTable(jsonUpdate, jsonWhere, "ID_CREATION_RULE");
                jsonUpdate.RemoveAll();
                jsonUpdate = null;
            }
            return nSeq;
        }
        
        public Boolean InsertSlip(JObject jsonSlip , JArray arrItems , JObject jsonSlipDocs)
        {
            Boolean bRet = true;
            try {
                //전표저장
                jsonSlipDocs.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd"));
                if (!Constants.LDB_MANAGER.ExitsTable("REFUNDSLIP"))
                {
                    Constants.LDB_MANAGER.createTable(jsonSlip, "REFUNDSLIP");//DB생성
                }

                if (!Constants.LDB_MANAGER.ExitsTable("SALES_GOODS"))
                {
                    if(arrItems != null && arrItems.Count >0)
                        Constants.LDB_MANAGER.createTable((JObject)arrItems[0], "SALES_GOODS");//DB생성
                }

                if (!Constants.LDB_MANAGER.ExitsTable("SLIP_PRINT_DOCS"))
                {
                    Constants.LDB_MANAGER.createTable(jsonSlipDocs, "SLIP_PRINT_DOCS");//DB생성
                }

                //전표 저장
                //Constants.SQLITE_MANAGER.InsertTable(jsonSlip, "REFUNDSLIP", 2);
                Constants.LDB_MANAGER.InsertTable(jsonSlip, "REFUNDSLIP");

                int insert_Flag = 2;
                //품목저장
                if (arrItems != null)
                {
                    for (int i = 0; i < arrItems.Count; i++)
                    {
                        if (arrItems.Count == 1) insert_Flag = 2;
                        else
                        {
                            if (i == 0) insert_Flag = 0;
                            else if (i == (arrItems.Count - 1)) insert_Flag = 1;
                            else insert_Flag = 2;
                        }
                        JObject tempObj = (JObject)arrItems[i];
                        tempObj.Remove("SHOP_NO");
                        //Constants.SQLITE_MANAGER.InsertTable(tempObj, "SALES_GOODS", insert_Flag);
                        Constants.LDB_MANAGER.InsertTable(tempObj, "SALES_GOODS");
                    }
                }

                //출력내용 저장
                //Constants.SQLITE_MANAGER.InsertTable(jsonSlipDocs, "SLIP_PRINT_DOCS", 2);
                Constants.LDB_MANAGER.InsertTable(jsonSlipDocs, "SLIP_PRINT_DOCS");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                bRet = false;
            }
            return bRet;
        }

        public Boolean InsertSlip(JObject jsonSlip, JObject jsonSlipDocs)
        {
            Boolean bRet = true;
            try
            {
                //전표저장
                jsonSlipDocs.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd"));
                if (!Constants.LDB_MANAGER.ExitsTable("REFUNDSLIP"))
                {
                    Constants.LDB_MANAGER.createTable(jsonSlip, "REFUNDSLIP");//DB생성
                }

                if (!Constants.LDB_MANAGER.ExitsTable("SLIP_PRINT_DOCS"))
                {
                    jsonSlipDocs.Add("GOODS", "");
                    Constants.LDB_MANAGER.createTable(jsonSlipDocs, "SLIP_PRINT_DOCS");//DB생성
                }

                //전표 저장
                //Constants.SQLITE_MANAGER.InsertTable(jsonSlip, "REFUNDSLIP", 2);
                Constants.LDB_MANAGER.InsertTable(jsonSlip, "REFUNDSLIP");

                //출력내용 저장
                //Constants.SQLITE_MANAGER.InsertTable(jsonSlipDocs, "SLIP_PRINT_DOCS", 2);
                Constants.LDB_MANAGER.InsertTable(jsonSlipDocs, "SLIP_PRINT_DOCS");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                bRet = false;
            }
            return bRet;
        }

        public Boolean InsertSlipSign(JObject jsonSlipSing)
        {
            Boolean bRet = true;
            try
            {
                if (!Constants.LDB_MANAGER.ExitsTable("REFUND_SLIP_SIGN"))
                {
                    Constants.LDB_MANAGER.createTable(jsonSlipSing, "REFUND_SLIP_SIGN");//DB생성
                }

                //Constants.SQLITE_MANAGER.InsertTable(jsonSlipSing, "REFUND_SLIP_SIGN", 2);
                Constants.LDB_MANAGER.InsertTable(jsonSlipSing, "REFUND_SLIP_SIGN");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                bRet = false;
            }
            return bRet;
        }

        //전표 테이블 내용 업데이트
        public Boolean UpdateSlip(JObject jsonSlip, JObject jsonFlag)
        {
            Boolean bRet = true;
            try
            {
                Constants.LDB_MANAGER.updateTable(jsonFlag, jsonSlip, Constants.TABLE_REFUND_SLIP);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                bRet = false;
            }
            return bRet;
        }

        //전표 조회
        //로컬DB 조회 또는 서버 조회 양쪽 공용으로 사용하기 위해서...
        public JArray SearchSlips(string strStartDate, string strEndDate, string strSendFlag, string strSlipStatus = "", string strSlipNo = "")
        {
            JArray jsonRes = null;
            if (Constants.LDB_MANAGER.ExitsTable(Constants.TABLE_REFUND_SLIP))
            {
                
                if (strStartDate == null || string.Empty.Equals(strStartDate))
                {
                    strStartDate = "20010101";
                }
                if (strEndDate == null || string.Empty.Equals(strEndDate))
                {
                    strEndDate = "29991231";
                }
                jsonRes = Constants.LDB_MANAGER.getSlipList("", Constants.TABLE_REFUND_SLIP, strStartDate, strEndDate
                    , "ALL".Equals(strSendFlag.Trim()) ? "" : strSendFlag.Trim()
                    , "ALL".Equals(strSlipStatus.Trim()) ? "" : strSlipStatus.Trim() 
                    , strSlipNo
                    );
                //jsonRes = Constants.LDB_MANAGER.SelectTable(Constants.TABLE_REFUND_SLIP, jsonWhere);
            }
            return jsonRes;
        }


        //전표 조회
        //로컬DB 조회 또는 서버 조회 양쪽 공용으로 사용하기 위해서...
        public JArray SearchUserSlips(string strStartDate, string strEndDate, string strSendFlag, string strSlipStatus = "", string strSlipNo = "")
        {
            JArray jsonRes = null;
            if (Constants.LDB_MANAGER.ExitsTable(Constants.TABLE_REFUND_SLIP))
            {

                if (strStartDate == null || string.Empty.Equals(strStartDate))
                {
                    strStartDate = "20010101";
                }
                if (strEndDate == null || string.Empty.Equals(strEndDate))
                {
                    strEndDate = "29991231";
                }
                jsonRes = Constants.LDB_MANAGER.getSlipList(Constants.USER_ID, Constants.TABLE_REFUND_SLIP, strStartDate, strEndDate
                    , "ALL".Equals(strSendFlag.Trim()) ? "" : strSendFlag.Trim()
                    , "ALL".Equals(strSlipStatus.Trim()) ? "" : strSlipStatus.Trim()
                    , strSlipNo
                    );
                //jsonRes = Constants.LDB_MANAGER.SelectTable(Constants.TABLE_REFUND_SLIP, jsonWhere);
            }
            return jsonRes;
        }


        //세부항목조회
        public JArray SearchSlipDetail(string strSlipNo)
        {
            JArray arrJsonRes = null;
            JObject jsonWhere = new JObject();
            jsonWhere.Add("SLIP_NO", strSlipNo);
            arrJsonRes = Constants.LDB_MANAGER.SelectTable("SALES_GOODS", jsonWhere);
            jsonWhere.RemoveAll();
            return arrJsonRes;
        }

        //로컬DB 설정값 조회
        public string SearchConfig()
        {
            string strRes = string.Empty;
            return strRes;
        }

        //가장 마지막에 접속한 사용자 정보 Get
        public JArray getLastUserInfo()
        {
            //JObject jsonRes = null;
            JArray arrjsonRes = new JArray();
            arrjsonRes = Constants.LDB_MANAGER.SelectTable("USERINFO");
            return arrjsonRes;
        }

        //가장 마지막에 접속한 사용자 정보 업데이트
        public string setLastUserInfo(JObject jsonRes)
        {
            string strRes = string.Empty;
            return strRes;
        }

        //UPI Bin 체크
        public Boolean Upi_BinCHeck(string strCardNo)
        {
            Boolean bRet = false;
            strCardNo = strCardNo.Replace("-", "");
            JArray arrjsonRes = new JArray();
            arrjsonRes = Constants.LDB_MANAGER.SelectUpiBinTable("BIN" , strCardNo);
            if (arrjsonRes != null && arrjsonRes.Count > 0)
                bRet = true;
            return bRet;
        }

        //UPI Bin 체크
        public Boolean QQ_Check(string strQQID)
        {
            Boolean bRet = true;
            decimal tempDec = 0;
            //데이터 체크
            if(strQQID == null || string.Empty.Equals(strQQID.Trim()) || !decimal.TryParse(strQQID, out tempDec))
            {
                bRet = false;
            }
            //값 범위
            if (tempDec < 10000 || tempDec > 4294967295)
            {
                bRet = false;
            }
            return bRet;
        }


        //서버 송신용 전문 생성
        public string BuildSlipDoc(string strSlipNo)
        {
            string strRet = "";
            //임시데이터 
            JObject jsonRet = null;
            JObject jsonRetItem = null;

            //where 조건
            JObject jsonWhere = new JObject();
            jsonWhere.Add("SLIP_NO", strSlipNo);
            //jsonWhere.Add("USERID", Constants.USER_ID);

            //return 용
            JObject jsonSlip = new JObject();
            JObject SlipRet = new JObject();
            JArray ArrSlipRet = new JArray();
            JArray ArrItemsRet = new JArray();

            //로컬DB 조회 결과
            JArray arrSlip = Constants.LDB_MANAGER.SelectTable("REFUNDSLIP", jsonWhere);
            JArray ArrSlipItems = Constants.LDB_MANAGER.SelectTable("SALES_GOODS", jsonWhere);

            if (arrSlip.Count > 0)
            {
                for (int j = 0; j < arrSlip.Count; j++)
                {
                    ArrSlipRet.Clear();
                    ArrItemsRet.Clear();
                    jsonRet = (JObject)arrSlip[j];
                    SlipRet.Add("buyer_name", jsonRet["BUYER_NAME"].ToString());
                    SlipRet.Add("passport_serial_no", jsonRet["PASSPORT_SERIAL_NO"].ToString());
                    SlipRet.Add("nationality_code", jsonRet["NATIONALITY_CODE"].ToString());
                    SlipRet.Add("gender_code", jsonRet["GENDER_CODE"].ToString());
                    SlipRet.Add("buyer_birth", jsonRet["BUYER_BIRTH"].ToString());
                    SlipRet.Add("pass_expirydt", jsonRet["PASS_EXPIRYDT"].ToString());
                    SlipRet.Add("input_way_code", jsonRet["INPUT_WAY_CODE"].ToString()); //여권스캐너 입력
                    SlipRet.Add("residence_no", jsonRet["RESIDENCE_NO"].ToString());
                    SlipRet.Add("entrydt", jsonRet["ENTRYDT"].ToString()); //상륙연월일
                    SlipRet.Add("passport_type", jsonRet["PASSPORT_TYPE"].ToString());
                    SlipRet.Add("slip_no", jsonRet["SLIP_NO"].ToString());
                    SlipRet.Add("merchant_no", jsonRet["MERCHANT_NO"].ToString());
                    SlipRet.Add("out_div_code", jsonRet["OUT_DIV_CODE"].ToString());
                    SlipRet.Add("refund_way_code", jsonRet["REFUND_WAY_CODE"].ToString());
                    SlipRet.Add("slip_status_code", jsonRet["SLIP_STATUS_CODE"].ToString());
                    SlipRet.Add("tml_id", jsonRet["TML_ID"].ToString());
                    SlipRet.Add("refund_cardno", jsonRet["REFUND_CARDNO"].ToString());
                    SlipRet.Add("refund_card_code", jsonRet["REFUND_CARD_CODE"].ToString());
                    SlipRet.Add("total_slipseq", jsonRet["TOTAL_SLIPSEQ"].ToString());
                    SlipRet.Add("tax_proc_time_code", jsonRet["TAX_PROC_TIME_CODE"].ToString());


                    SlipRet.Add("unikey", jsonRet["UNIKEY"].ToString());

                    SlipRet.Add("saledt", jsonRet["SALEDT"].ToString());
                    SlipRet.Add("refunddt", jsonRet["REFUNDDT"].ToString());
                    SlipRet.Add("userId", jsonRet["USERID"].ToString());
                    SlipRet.Add("merchantNo", jsonRet["MERCHANTNO"].ToString());
                    SlipRet.Add("deskId", jsonRet["DESKID"].ToString());
                    SlipRet.Add("companyID", jsonRet["COMPANYID"].ToString());
                    SlipRet.Add("permit_no", jsonRet["PERMIT_NO"].ToString());
                    SlipRet.Add("refund_note", jsonRet["REFUND_NOTE"].ToString());

                    ArrSlipRet.Add(SlipRet);
                    //1. 전표정보
                    long total_comm_sale_amt = 0;
                    long total_comm_tax_amt = 0;
                    long total_comm_refund_amt = 0;
                    long total_comm_fee_amt = 0;

                    long total_excomm_sale_amt = 0;
                    long total_excomm_tax_amt = 0;
                    long total_excomm_refund_amt = 0;
                    long total_excomm_fee_amt = 0;


                    //2. 물품정보
                    for (int i = 0; i < ArrSlipItems.Count; i++)
                    {
                        JObject tempObj = new JObject();
                        jsonRetItem = (JObject)ArrSlipItems[i];

                        tempObj.Add("slip_no", strSlipNo);
                        tempObj.Add("sale_seq", jsonRetItem["ITEM_NO"].ToString());
                        tempObj.Add("rec_no", jsonRetItem["RCT_NO"].ToString());
                        tempObj.Add("tax_amt", jsonRetItem["TAX_AMT"].ToString());
                        tempObj.Add("refund_amt", jsonRetItem["REFUND_AMT"].ToString());
                        tempObj.Add("fee_amt", jsonRetItem["FEE_AMT"].ToString());
                        tempObj.Add("goods_items_code", jsonRetItem["ITEM_TYPE"].ToString());
                        tempObj.Add("goods_group_code", jsonRetItem["MAIN_CAT"].ToString());
                        //tempObj.Add("goods_division", jsonRetItem["MID_CAT"].ToString());
                        tempObj.Add("goods_division", "C0167");
                        //tempObj.Add("tax_proc_time_code", jsonRet["TAX_PROC_TIME_CODE"].ToString());
                        tempObj.Add("tax_proc_time_code", jsonRetItem["TAX_TYPE"].ToString());
                        tempObj.Add("goods_amt", jsonRetItem["BUY_AMT"].ToString());
                        tempObj.Add("tax_point_proc_code", jsonRet["TAX_POINT_PROC_CODE"].ToString());
                        tempObj.Add("goods_qty", jsonRetItem["QTY"].ToString());
                        tempObj.Add("goods_unit_price", jsonRetItem["UNIT_AMT"].ToString());
                        tempObj.Add("goods_name", jsonRetItem["ITEM_NAME"].ToString());
                        tempObj.Add("goods_tax_code", "SCT");
                        tempObj.Add("same_unit_saleAmt", "Y");
                        tempObj.Add("worker_id", Constants.USER_ID);
                        tempObj.Add("tax_type", jsonRetItem["TAX_CAL_TYPE"].ToString());
                        ////소비용품
                        //if ("A0002".Equals(jsonRetItem["ITEM_TYPE"].ToString()))
                        //{
                        //    total_comm_sale_amt += Int64.Parse(jsonRetItem["BUY_AMT"].ToString());    //소비용품 판매액 합산 //(Int64.Parse(jsonRetItem["BUY_AMT"].ToString()) - Int64.Parse(jsonRetItem["TAX_AMT"].ToString()));//내세토탈 판매액
                        //    total_comm_tax_amt += Int64.Parse(jsonRetItem["TAX_AMT"].ToString());   //내세 세금합산
                        //    total_comm_refund_amt += Int64.Parse(jsonRetItem["REFUND_AMT"].ToString()); //내세 환급금 합산
                        //}
                        //else
                        //{
                        //    total_excomm_sale_amt += Int64.Parse(jsonRetItem["BUY_AMT"].ToString());    //외세 판매액 합산
                        //    total_excomm_tax_amt += Int64.Parse(jsonRetItem["TAX_AMT"].ToString());     //외세 세금 합산
                        //    total_excomm_refund_amt += Int64.Parse(jsonRetItem["REFUND_AMT"].ToString());   //외세 환급금 합산
                        //}
                        ArrItemsRet.Add(tempObj);
                    }


                    total_excomm_sale_amt = Int64.Parse(jsonRet["GOODS_BUY_AMT"].ToString());
                    total_excomm_tax_amt = Int64.Parse(jsonRet["GOODS_TAX_AMT"].ToString());
                    total_excomm_refund_amt = Int64.Parse(jsonRet["GOODS_REFUND_AMT"].ToString());

                    total_comm_sale_amt = Int64.Parse(jsonRet["CONSUMS_BUY_AMT"].ToString());
                    total_comm_tax_amt = Int64.Parse(jsonRet["CONSUMS_TAX_AMT"].ToString());
                    total_comm_refund_amt = Int64.Parse(jsonRet["CONSUMS_REFUND_AMT"].ToString());

                    SlipRet.Add("total_excomm_in_tax_sale_amt", jsonRet["TOTAL_EXCOMM_IN_TAX_SALE_AMT"].ToString());
                    SlipRet.Add("total_comm_in_tax_sale_amt", jsonRet["TOTAL_COMM_IN_TAX_SALE_AMT"].ToString());

                    total_comm_fee_amt = total_comm_tax_amt - total_comm_refund_amt;//소비용품 총 수수료
                    total_excomm_fee_amt = total_excomm_tax_amt - total_excomm_refund_amt;//일반물품 총 수수료

                    //소비용품
                    SlipRet.Add("comm", total_comm_sale_amt > 0 ? "A0002" : "");
                    SlipRet.Add("total_comm_sale_amt", total_comm_sale_amt.ToString());
                    SlipRet.Add("total_comm_tax_amt", total_comm_tax_amt.ToString());
                    SlipRet.Add("total_comm_refund_amt", total_comm_refund_amt.ToString());
                    SlipRet.Add("total_comm_fee_amt", total_comm_fee_amt.ToString());

                    //일반용품
                    SlipRet.Add("excomm", total_excomm_sale_amt > 0 ? "A0001" : "");
                    SlipRet.Add("total_excomm_sale_amt", total_excomm_sale_amt.ToString());
                    SlipRet.Add("total_excomm_tax_amt", total_excomm_tax_amt.ToString());
                    SlipRet.Add("total_excomm_refund_amt", total_excomm_refund_amt.ToString());
                    SlipRet.Add("total_excomm_fee_amt", total_excomm_fee_amt.ToString());

                    //if (bInTax)
                    //{
                    //    SlipRet.Add("total_excomm_in_tax_sale_amt", (total_excomm_sale_amt - total_excomm_tax_amt).ToString());
                    //    SlipRet.Add("total_comm_in_tax_sale_amt", (total_comm_sale_amt - total_comm_tax_amt).ToString());
                    //}else
                    //{
                    //    SlipRet.Add("total_excomm_in_tax_sale_amt", (total_excomm_sale_amt ).ToString());
                    //    SlipRet.Add("total_comm_in_tax_sale_amt", (total_comm_sale_amt  ).ToString());
                    //}
                    SlipRet.Add("remit_amt", (total_comm_refund_amt + total_excomm_refund_amt).ToString());
                    
                    SlipRet.Add("saleGoodsList", ArrItemsRet);
                    jsonSlip.Add("slipList", ArrSlipRet); 
                }
                jsonSlip.Add("userId", Constants.USER_ID);
                jsonSlip.Add("companyID", Constants.COMPANY_ID);
                jsonSlip.Add("languageCD", Constants.LANGUAGECD);
                jsonSlip.Add("merchantNo", Constants.MERCHANT_NO);
                jsonSlip.Add("deskId", Constants.DESK_ID);
                strRet = jsonSlip.ToString();
            }
            
            return strRet; 
        }

        //전표출력용 전문 Get
        public JObject GetPrintDocs(string strSlipNo)
        {
            JObject jsonRet = null;
            JObject jsonWhere = new JObject();
            jsonWhere.Add("SLIP_NO", strSlipNo);
            JArray arrSlip = Constants.LDB_MANAGER.SelectTable("SLIP_PRINT_DOCS", jsonWhere);
            if(arrSlip != null && arrSlip.Count > 0)
            {
                jsonRet = (JObject)arrSlip[0];
            }
            
            return jsonRet;
        }

        public Boolean NetworkPing()
        {
            Boolean bRet = true;
            GTF_CommManager comm = new GTF_CommManager(null);
            bRet = comm.sendPing(Constants.SERVER_URL);
            return bRet;
        }

        public bool checkNetworkStatus(String strUserId)
        {
            Boolean bRet = true;
            string strReq = string.Empty;
            string strRes = string.Empty;
            JObject jsonRes = null;
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                JObject jsonReq = new JObject();
                jsonReq.Add("userId", strUserId);
                jsonReq.Add("openDate", Constants.OPEN_DATE); //오늘날짜
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("companyID", "000001");

                strReq = jsonReq.ToString();
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Status_check, strReq, true);

                if (strRes == null || string.Empty.Equals(strRes.Trim()))
                {
                    //처리오류
                    bRet = false;
                }
                else
                {
                    jsonRes = JObject.Parse(strRes);
                    if ("S".Equals((string)jsonRes["result"]))
                    {
                        //마감취소성공
                        bRet = true;
                    }
                    else
                    {
                        //마감취소실패
                        bRet = false;
                    }
                }
            }
            catch (Exception e)
            {
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return bRet;
        }

        public string sendSlipSignData()
        {
            string strRet = "";
            string strReq = "";
            JObject jsonRet = null;
            Boolean table_ex = false;
            GTF_CommManager comm = new GTF_CommManager(null);

            table_ex = Constants.LDB_MANAGER.ExitsTable("REFUND_SLIP_SIGN");

            if (table_ex)
            {

                //where 조건
                JObject jsonWhere = new JObject();
                jsonWhere.Add("SEND_YN", "N");


                JArray arrSlip = Constants.LDB_MANAGER.SelectTable("REFUND_SLIP_SIGN", jsonWhere);
                //Console.WriteLine("SIGN 전송대상[" + arrSlip.Count + "]");

                if (arrSlip.Count > 0)
                {
                    for (int j = 0; j < arrSlip.Count; j++)
                    {
                        jsonRet = (JObject)arrSlip[j];
                        //Console.WriteLine("SIGN 전표번호[" + jsonRet["SLIP_NO"].ToString() + "]");
                        JObject SlipSign = new JObject();
                        SlipSign.Add("slip_no", jsonRet["SLIP_NO"].ToString());
                        SlipSign.Add("sign_data", jsonRet["SLIP_SIGN_DATA"].ToString());
                        SlipSign.Add("userId", Constants.USER_ID);
                        strReq = SlipSign.ToString();
                        string strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_Sign, strReq, true);
                        if (strRes == null || string.Empty.Equals(strRes.Trim()))
                        {

                        }
                        else
                        {
                            JObject resultJson = new JObject();
                            resultJson = JObject.Parse(strRes);
                            if ("S".Equals((string)resultJson["result"]))
                            {
                                JObject jsonSignWhere = new JObject();
                                JObject tmpObj = new JObject();
                                tmpObj.Add("SEND_YN", "Y");
                                jsonSignWhere.Add("SLIP_NO", jsonRet["SLIP_NO"].ToString());

                                Constants.LDB_MANAGER.updateTable(tmpObj, jsonSignWhere, "REFUND_SLIP_SIGN");
                            }

                        }

                    }
                }
            }
            return strRet;
        }

        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            System.Net.ServicePointManager.ServerCertificateValidationCallback
             = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }
        private static bool AcceptAllCertifications
        (
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        }

        //온라인 거래 조회
        public string onlineSearch(string ReqData)
        {
            string strRes = string.Empty;
            JObject resultJson = new JObject();
            
            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                //Constants.LOGGER_DOC.Info("-->" + ReqData.ToString());
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_ServerSlip, ReqData.ToString(), true);
                //Constants.LOGGER_DOC.Info("<--" + strRes);

                resultJson = JObject.Parse(strRes);
                strRes = resultJson["list"].ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("onlineSearch Exception");
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return strRes;
        }

        //온라인 거래 조회 총 건수
        public int onlineSearchCount(string jsonReq)
        {
            int total_cnt = 0;
            string strRes = string.Empty;
            JObject resultJson = new JObject();

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                //Constants.LOGGER_DOC.Info("-->" + jsonReq.ToString());
                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_ServerSlipCount, jsonReq.ToString(), true);
                //Constants.LOGGER_DOC.Info("<--" + strRes);
                
                resultJson = JObject.Parse(strRes);
                strRes = resultJson["total_cnt"].ToString();

                total_cnt = int.Parse(strRes);
            }
            catch (Exception e)
            {
                Console.WriteLine("onlineSearch Exception");
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return total_cnt;
        }

        //온라인 상세 조회
        public string onlineSearchDetail(string ReqData)
        {
            string strRes = string.Empty;
            JObject resultJson = new JObject();

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_ServerSlipDetail, ReqData.ToString(), true);
               
                resultJson = JObject.Parse(strRes);
                strRes = resultJson["list"].ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("onlineSearchDetail Exception");
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return strRes;
        }

        //온라인 물품 상세 조회
        public string onlineSearchGoodsDetail(string ReqData)
        {
            string strRes = string.Empty;
            JObject resultJson = new JObject();

            try
            {
                GTF_CommManager comm = new GTF_CommManager(null);

                strRes = comm.sendHttp_Json(Constants.SERVER_URL + url_ServerSlipGoodsDetail, ReqData.ToString(), true);

                resultJson = JObject.Parse(strRes);
                strRes = resultJson["list"].ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("onlineSearchGoodsDetail Exception");
                Constants.LOGGER_MAIN.Info(e.Message);
            }
            return strRes;
        }

        public bool checkTable(string tabaleName)
        {
            bool exits_table = false;

            exits_table = Constants.LDB_MANAGER.ExitsTable(tabaleName);

            return exits_table;
        }

        public JObject getRCTInfo(string strSlipNO = "")
        {
            JObject jsonRet = null;
            if(strSlipNO != "")
            {
                jsonRet = Constants.LDB_MANAGER.getRCTInfo(strSlipNO);
            }
            else
            {
                jsonRet.Add("RCT_NO", "");
            }
            return jsonRet;
        }

    }
}
