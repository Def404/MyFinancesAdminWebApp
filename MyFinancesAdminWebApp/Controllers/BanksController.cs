using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyFinancesAdminWebApp.Context;
using MyFinancesAdminWebApp.Models;

namespace MyFinancesAdminWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class BanksController : Controller
    {
        private MyfinancesContext _context;

        public BanksController(MyfinancesContext context) {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var banks = _context.Banks.Select(i => new {
                i.BankId,
                i.Name,
                i.Colour
            });
            
            return Json(await DataSourceLoader.LoadAsync(banks, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            
            var model = new Bank();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Banks.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.BankId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Banks.FirstOrDefaultAsync(item => item.BankId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Banks.FirstOrDefaultAsync(item => item.BankId == key);

            _context.Banks.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Bank model, IDictionary values) {
            string BANK_ID = nameof(Bank.BankId);
            string NAME = nameof(Bank.Name);
            string COLOUR = nameof(Bank.Colour);

            if(values.Contains(BANK_ID)) {
                model.BankId = Convert.ToInt32(values[BANK_ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(COLOUR)) {
                model.Colour = Convert.ToString(values[COLOUR]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}