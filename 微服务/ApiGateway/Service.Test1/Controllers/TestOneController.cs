using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Common;
using Newtonsoft.Json;
using Service.Test1.EF6Core;
using Microsoft.EntityFrameworkCore;
using Service.Test1.Model;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Service.Test1.Controllers
{
    [Route("api1/[controller]")]
    [ApiController]
    public class TestOneController : BaseAPIController
    {
        private readonly TodoContext context;
        private ReposeCommon common;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TestOneController(IConfiguration configuration, TodoContext context, IHostingEnvironment hostingEnvironment) : base(configuration)
        {
            this.context = context;
            if (context.TodoItems.Count() == 0)
            {
                this.context.TodoItems.Add(new TodoItem() { Name = "Item1" });
                this.context.SaveChanges();
            }
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetTodoItems")]
        public async Task<ActionResult<ReposeCommon>> GetTodoItems()
        {
            common = new ReposeCommon();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                var result = _hostingEnvironment.ContentRootPath + "/Json/test1.json";
                var json = System.IO.File.ReadAllText(result);
                var model = JsonConvert.DeserializeObject<List<object>>(json);

                foreach (object item in model)
                {

                }

                List<TodoItem> items = await context.TodoItems.ToListAsync();
                common.code = ResponseStatu.success;
                common.data = items;
                common.total = items.Count.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(log, ex);
                common.code = ResponseStatu.fail;
                common.msg = "服务器错误";
            }
            return common;
        }


        [HttpGet("GetTodoItem/{id}")]
        public async Task<ActionResult<ReposeCommon>> GetTodoItem(long id)
        {
            common = new ReposeCommon();
            try
            {
                string json = "\"{\\\"tmall_exchange_receive_get_response\\\":{\\\"has_next\\\":false,\\\"page_results\\\":0,\\\"total_results\\\":0,\\\"request_id\\\":\\\"nepu8r5yyyy2\\\"}}\"\n";
                JsonConvert.DeserializeObject<dynamic>(json);

                string newJson = JsonConvert.DeserializeObject<object>(json).ToString();
                var model = JsonConvert.DeserializeObject<Root>(newJson);

                var todoItem = await context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    common.code = ResponseStatu.success;
                    common.msg = "未找到记录";
                    return common;
                }

                common.data = todoItem;
                common.code = ResponseStatu.success;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(log, ex);
                common.code = ResponseStatu.fail;
                common.msg = "服务器错误";
            }
            return common;
        }

        [HttpPost("PostTodoItem")]
        public async Task<ActionResult<ReposeCommon>> PostTodoItem(TodoItem item)
        {
            common = new ReposeCommon();
            try
            {
                await context.TodoItems.AddAsync(item);
                int result = await context.SaveChangesAsync();

                if (result > 0)
                {
                    common.msg = "操作成功";
                    common.code = ResponseStatu.success;
                }
                else
                {
                    common.msg = "操作失败";
                    common.code = ResponseStatu.fail;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(log, ex);
                common.code = ResponseStatu.fail;
                common.msg = "服务器错误";
            }
            return common;
        }

        [HttpPut("PutTodoItem/{id}")]
        public async Task<ActionResult<ReposeCommon>> PutTodoItem(long id, TodoItem item)
        {
            common = new ReposeCommon();
            try
            {
                if (id != item.Id)
                {
                    common.msg = "参数有误，请重新操作";
                    common.code = ResponseStatu.fail;
                    return common;
                }

                context.Entry(item).State = EntityState.Modified;
                int result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    common.msg = "操作成功";
                    common.code = ResponseStatu.success;
                }
                else
                {
                    common.msg = "操作失败";
                    common.code = ResponseStatu.fail;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(log, ex);
                common.code = ResponseStatu.fail;
                common.msg = "服务器错误";
            }
            return common;
        }

        [HttpDelete("DeleteTodoItem/{id}")]
        public async Task<ActionResult<ReposeCommon>> DeleteTodoItem(long id)
        {
            common = new ReposeCommon();
            try
            {
                var item = await context.TodoItems.FindAsync(id);
                if (item == null)
                {
                    common.code = ResponseStatu.fail;
                    common.msg = "未找到记录";
                    return common;
                }

                context.TodoItems.Remove(item);
                int result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    common.msg = "操作成功";
                    common.code = ResponseStatu.success;
                }
                else
                {
                    common.data = "操作失败";
                    common.code = ResponseStatu.fail;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(log, ex);
                common.code = ResponseStatu.fail;
                common.msg = "服务器错误";
            }
            return common;
        }
    }
}
