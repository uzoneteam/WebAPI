using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting;
using AttributeRouting.Web.Http;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using WebAPI.Models;


namespace PromotionServices_WebAPI.Models
{
    [RoutePrefix("api")]
    public class MobileController : ApiController
    {
        #region GETs

        /// <summary>
        /// Get all registered schools
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpOptions]
        [GET("schools")]
        public HttpResponseMessage GetRegisteredSchools()
        {
            List<School> _schools = null;
            using (var context = new uzoneEntities())
            {
                _schools = (from s in context.Schools
                            select s).ToList();

                if (_schools.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, new { _schools });
            }
        }

        /// <summary>
        /// Get events by school and month
        /// </summary>
        /// <param name="schoolID"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpOptions]
        [GET("{schoolID}/events/{month}")]
        public HttpResponseMessage GetSchoolEventsByMonth([FromUri] string schoolID, [FromUri] string month)
        {
            List<Scheduler> _scheduler = null;        
            long sID = Convert.ToInt64(schoolID);
            int viewingMonth = Convert.ToInt32(month);
            using (var context = new uzoneEntities())
            {
                _scheduler = (from s in context.Schedulers
                              where s.SchoolID == sID &&
                              s.EventStart.Value.Month == viewingMonth &&
                              s.EventStart.Value.Year == DateTime.Now.Year 
                              //s.EventStart >= DateTime.Now
                              select s).ToList();

                if (_scheduler.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK,  _scheduler );
            }
        }

        /// <summary>
        /// Get events by school inital call
        /// </summary>
        /// <param name="schoolID"></param>      
        /// <returns></returns>
        [HttpGet]
        [HttpOptions]        
        [GET("events/{schoolID}")]
        public HttpResponseMessage GetSchoolCurrentEvents([FromUri] string schoolID)
        {
            List<Scheduler> _scheduler = null;
            long sID = Convert.ToInt64(schoolID);
            int currentMonth = DateTime.Now.Month;
            using (var context = new uzoneEntities())
            {
                _scheduler = (from s in context.Schedulers
                              where s.SchoolID == sID &&
                              s.EventStart.Value.Month == currentMonth &&
                              s.EventStart.Value.Year == DateTime.Now.Year &&
                              s.EventStart >= DateTime.Now
                              select s).ToList();

                if (_scheduler.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, _scheduler);
            }
        }

        #endregion GETs

    }
}