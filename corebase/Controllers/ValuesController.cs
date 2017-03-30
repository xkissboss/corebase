using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using db.BLL;
using corebase.Util;
using db.Model;
using common.CSRedis;

namespace corebase.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {

        private IStudentBLL _studentBLL;

        public ValuesController(ILoggerFactory factory, IServiceProvider svp, IStudentBLL studentBLL) : base(factory, svp)
        {
            this._studentBLL = studentBLL;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            RedisHelper.Set("hello", "world");
            return new string[] { "value1", "value2" };

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public APIReturn Get(long id)
        {

            string hello = RedisHelper.Get("hello");

            if (id < 1)
                return APIReturn.BuildFail("id正确");
            Student s = RedisHelper.GetEntity<Student>(id.ToString());
            if (s != null)
                return APIReturn.BuildSuccess(s);
            s = _studentBLL.FindById(id);
            if (s == null)
                return APIReturn.BuildFail("记录不存在");
            RedisHelper.SetEntity<Student>(s.Sid.ToString(), s);
            return APIReturn.BuildSuccess(s);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
