using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml;

using DotaBrackets_WEB_2016.Models;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Web.Security;
using System.Security.Cryptography;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class AccountDataController
    {

        public static SqlDatabase db;

        public AccountDataController()
        {
            if (db == null)
            {
                db = new SqlDatabase(WebConfigurationManager.ConnectionStrings["DotaBracketsConnectionString"].ToString());
            }
        }

        //hash passwords method
        public static string GetSwcSH1(string access)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(access));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }

    //*************************************************** Login a User ********************************************************
        //login an existing user
        public ViewModel Login(ViewModel viewModel)
        {
            try
            {
                //hash incoming password
                viewModel.gamer.access = GetSwcSH1(viewModel.gamer.access);

                //create a dataSet to hold incoming data
                DbCommand dbCommand = db.GetStoredProcCommand("get_Login");
                db.AddInParameter(dbCommand, "userName", DbType.String, viewModel.gamer.userName);
                db.AddInParameter(dbCommand, "access", DbType.String, viewModel.gamer.access);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataRow drRow = ds.Tables[0].Rows[0];

                viewModel.gamer = new Gamer()
                {
                    gamerID = drRow.Field<int>("gamerID"),
                    userName = drRow.Field<string>("userName"),
                    steamID = drRow.Field<long>("steamID"),
                    dota2ID = drRow.Field<int>("dota2ID"),
                    isSearching = drRow.Field<bool>("isSearching"),
                    avatar = drRow.Field<string>("avatar")
                };

                viewModel.gamer.preferences = new Preferences()
                {
                    mmr = drRow.Field<int>("pmmr"),
                    hasMic = drRow.Field<int>("phasMic"),
                    language = drRow.Field<int>("plang"),
                };

                viewModel.gamer.traits = new Traits()
                {
                    mmr = drRow.Field<int>("tmmr"),
                    hasMic = drRow.Field<int>("thasMic"),
                    language = drRow.Field<int>("tlang"),
                    server = drRow.Field<int>("tserv")
                };
                
                viewModel.gamer.friendsList.friendsList = (from dr in ds.Tables[1].AsEnumerable()
                                   select new FriendID()
                                   {
                                       friendID = dr.Field<int>("friendID"),
                                       userName = dr.Field<string>("friendUserName"),
                                       steamID = dr.Field<long>("friendSteamID")
                                   }).ToList();

                if (viewModel.gamer.steamID == 0)
                {
                    ViewModel blankModel = new ViewModel();
                    return blankModel;
                }
                else
                {
                    return viewModel;
                }
            }
            catch
            {
                ViewModel blankModel = new ViewModel();
                return blankModel;
            }
        }

   //************************************************* Create new User ******************************************************
        //Creates a new user
        public string CreateUser(ViewModel viewModel)
        {
            try
            {
                //hash incoming password
                viewModel.gamer.access = GetSwcSH1(viewModel.gamer.access);

                ViewModel checkModel = new ViewModel();

                checkModel = Login(viewModel);

                if (checkModel == null)
                {

                    DbCommand dbCommand = db.GetStoredProcCommand("ins_Traits");

                    db.AddInParameter(dbCommand, "tlang", DbType.Int32, viewModel.gamer.traits.language);
                    db.AddInParameter(dbCommand, "thasMic", DbType.Int32, viewModel.gamer.traits.hasMic);
                    db.AddInParameter(dbCommand, "tserv", DbType.Int32, viewModel.gamer.traits.server);
                    db.AddInParameter(dbCommand, "tmmr", DbType.Int32, viewModel.gamer.traits.mmr);
                    db.AddOutParameter(dbCommand, "traitsID", DbType.Int32, sizeof(Int32));
                    db.ExecuteScalar(dbCommand);
                    int traitsID = (int)db.GetParameterValue(dbCommand, "@traitsID");


                    dbCommand = db.GetStoredProcCommand("ins_Preferences");

                    db.AddInParameter(dbCommand, "pmmr", DbType.Int32, viewModel.gamer.preferences.mmr);
                    db.AddInParameter(dbCommand, "phasMic", DbType.Int32, viewModel.gamer.preferences.hasMic);
                    db.AddInParameter(dbCommand, "plang", DbType.Int32, viewModel.gamer.preferences.language);
                    db.AddOutParameter(dbCommand, "preferencesID", DbType.Int32, sizeof(Int32));
                    db.ExecuteScalar(dbCommand);
                    int preferencesID = (int)db.GetParameterValue(dbCommand, "@preferencesID");


                    dbCommand = db.GetStoredProcCommand("ins_Gamer");
                    db.AddInParameter(dbCommand, "traitsID", DbType.Int32, traitsID);
                    db.AddInParameter(dbCommand, "preferencesID", DbType.Int32, preferencesID);
                    db.AddInParameter(dbCommand, "userName", DbType.String, viewModel.gamer.userName);
                    db.AddInParameter(dbCommand, "access", DbType.String, viewModel.gamer.access);
                    db.AddInParameter(dbCommand, "steamID", DbType.Int64, viewModel.gamer.steamID);
                    db.AddInParameter(dbCommand, "dota2ID", DbType.Int32, viewModel.gamer.dota2ID);
                    db.AddInParameter(dbCommand, "avatar", DbType.Int32, viewModel.gamer.avatar);
                    db.AddOutParameter(dbCommand, "gamerID", DbType.Int32, sizeof(Int32));
                    db.ExecuteScalar(dbCommand);
                    int gamerID = (int)db.GetParameterValue(dbCommand, "@gamerID");



                    dbCommand = db.GetStoredProcCommand("ins_FriendsList");

                    db.AddInParameter(dbCommand, "gamerID", DbType.Int32, gamerID);
                    db.ExecuteNonQuery(dbCommand);



                    return "success";
                }
                else
                {
                    return "exists";
                }
            }
            catch
            {
                return "failed";
            }
        }
    }
}