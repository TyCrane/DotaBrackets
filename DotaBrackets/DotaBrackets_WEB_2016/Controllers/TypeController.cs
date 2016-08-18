using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using System.Data.Common;
using System.Text;
using System.Xml;

using DotaBrackets_WEB_2016.Models;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DotaBrackets_WEB_2016.Controllers
{
    public class TypeController : Controller
    {
        public static SqlDatabase db;

        public TypeController()
        {
            if (db == null)
            {
                db = new SqlDatabase(WebConfigurationManager.ConnectionStrings["DotaBracketsConnectionString"].ToString());
            }
        }
    //****************************************** Methods to get Type Table Lists **********************************************
        //gets the mmr type 
        public SelectList GetMmrTypeList()
        {
            DbCommand dbCommand = db.GetStoredProcCommand("get_AllMmrType");

            DataSet ds = db.ExecuteDataSet(dbCommand);

            var selectList = (from drRow in ds.Tables[0].AsEnumerable()
                              select new SelectListItem()
                              {
                                  Text = drRow.Field<string>("mmrType"),
                                  Value = drRow.Field<int>("mmrTypeID").ToString()

                              }).ToList();

            return new SelectList(selectList, "Value", "Text");
        }


        //gets the hasMic type
        public SelectList GetHasMicType()
        {
            DbCommand dbCommand = db.GetStoredProcCommand("get_AllHasMicType");

            DataSet ds = db.ExecuteDataSet(dbCommand);

            var selectList = (from drRow in ds.Tables[0].AsEnumerable()
                              select new SelectListItem()
                              {
                                  Text = drRow.Field<string>("hasMicType"),
                                  Value = drRow.Field<int>("hasMicTypeID").ToString()

                              }).ToList();

            return new SelectList(selectList, "Value", "Text");
        }


        //gets the langType
        public SelectList GetLangType()
        {
            DbCommand dbCommand = db.GetStoredProcCommand("get_AllLangType");

            DataSet ds = db.ExecuteDataSet(dbCommand);

            var selectList = (from drRow in ds.Tables[0].AsEnumerable()
                              select new SelectListItem()
                              {
                                  Text = drRow.Field<string>("langType"),
                                  Value = drRow.Field<int>("langTypeID").ToString()

                              }).ToList();

            return new SelectList(selectList, "Value", "Text");
        }


        //gets the servType
        public SelectList GetServType()
        {
            DbCommand dbCommand = db.GetStoredProcCommand("get_AllServType");

            DataSet ds = db.ExecuteDataSet(dbCommand);

            var selectList = (from drRow in ds.Tables[0].AsEnumerable()
                              select new SelectListItem()
                              {
                                  Text = drRow.Field<string>("servType"),
                                  Value = drRow.Field<int>("servTypeID").ToString()

                              }).ToList();

            return new SelectList(selectList, "Value", "Text");
        }
    }
}
