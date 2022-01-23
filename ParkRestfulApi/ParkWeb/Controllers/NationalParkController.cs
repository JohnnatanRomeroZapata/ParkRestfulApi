using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkCore.Models.Dtos;
using ParkWeb.Repository.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> IndexNationalPark()
        {
            IEnumerable<NationalParkDto> listOfNationalParks = await GetAllNationalParks();

            return View(listOfNationalParks);
        }

        [HttpGet]
        public async Task<IEnumerable<NationalParkDto>> GetAllNationalParks()
        {
            //return Json(new { data = await _nationalParkRepository.GetAllAsync(StaticDetails.NationalParkAPIPath) });
            return  await _nationalParkRepository.GetAllAsync(StaticDetails.NationalParkAPIPath);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateNationalPark(int? id)
        {
            NationalParkDto nationalParkToCreateOrUpdate;

            if (id is null)
            {
                nationalParkToCreateOrUpdate = new NationalParkDto();
                return View(nationalParkToCreateOrUpdate);
            }

            nationalParkToCreateOrUpdate = await _nationalParkRepository.GetAsync(StaticDetails.NationalParkAPIPath, id.GetValueOrDefault());

            if(nationalParkToCreateOrUpdate is null)
            {
                return NotFound();
            }

            return View(nationalParkToCreateOrUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdateNationalPark(NationalParkDto nationalParkDto)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if(files.Count > 0)
                {
                    byte[] picture1 = null;

                    using(var fileString1 = files[0].OpenReadStream())
                    {
                        using(var memoryStream1 = new MemoryStream())
                        {
                            fileString1.CopyTo(memoryStream1);

                            picture1 = memoryStream1.ToArray();
                        }
                    }

                    nationalParkDto.Picture = picture1;
                }
                else
                {
                    if(nationalParkDto.Id > 0)
                    {
                        var nationalParkFromDb = await _nationalParkRepository.GetAsync(StaticDetails.NationalParkAPIPath, nationalParkDto.Id);
                        nationalParkDto.Picture = nationalParkFromDb.Picture;
                    }
                }

                if(nationalParkDto.Id == 0)
                {
                    TempData["Message"] = "National Park was created";
                    await _nationalParkRepository.CreateAsync(StaticDetails.NationalParkAPIPath, nationalParkDto);
                }
                else
                {
                    TempData["Message"] = "National Park was updated";
                    await _nationalParkRepository.UpdateAsync(StaticDetails.NationalParkAPIPath, nationalParkDto.Id ,nationalParkDto);
                }
                                
                return RedirectToAction(nameof(IndexNationalPark));
            }
            else
            {
                if(nationalParkDto.Id == 0)
                {
                    TempData["Message"] = "National Park was not created";
                    return View(nationalParkDto);
                }
                else
                {
                    TempData["Message"] = "National Park was not updated";
                    return View(nationalParkDto);
                }                
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNationalPark(int id)
        {
            bool objWasDeleted = await _nationalParkRepository.DeleteAsync(StaticDetails.NationalParkAPIPath, id);

            if (objWasDeleted)
            {
                return Json(new { success = true, message = "National Park was deleted" });
            }
            else
            {
                return Json(new { success = false, message = "National Park was not deleted" });
            }

        }
    }
}
