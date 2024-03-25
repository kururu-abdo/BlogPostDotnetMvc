using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagInterface _tagRepo;

        public AdminTagsController(ITagInterface tagRepo)
        {
           _tagRepo = tagRepo;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest request)
        {

            ValidateAddTagRequest(request);
            if (!ModelState.IsValid)
            {
                return View();

            }
            var name = request.Name;
            var displayName = request.DisplayName;
            var tag = new Tag
            {
                Name = name,
                DisplayName=displayName

            };
            await _tagRepo.AddAsync(tag);

            //return View("Add");
            return RedirectToAction("Tags");
        }





        [HttpGet]
        [ActionName("Tags")]
        public async Task<IActionResult> Tags()
        {

            //use dbContext to read tags
            var tags = await _tagRepo.GetAllAsync();

            return View(tags);
        }



        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {

            //use dbContext to read tags
            var tag =
               await _tagRepo.GetAsync(id);


            if (tag!=null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }


            return View(null);
        }







        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTag)
        {

            //use dbContext to read tags
            var tag = new Tag
            {
                Id = editTag.Id,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName
            };
            var updatedTag = await

                _tagRepo.UpdateAsync(tag);

            if (updatedTag != null)
            {
               
                //show success notification
                return RedirectToAction("Edit", new { id = editTag.Id });

            }else
            {

            }


            return RedirectToAction("Edit", new { id = editTag.Id });

            
        }


        [HttpPost]
        [ActionName("Delete")]
        public  async Task<IActionResult> Delete(EditTagRequest editTag)
        {
            var deletedTag = await _tagRepo.DeleteAsync(editTag.Id);
            if (deletedTag!=null)
            {
                //show success
            }else
            {


                //show error
            }
            return RedirectToAction("Edit" , new {id=editTag.Id});

        }

        private void ValidateAddTagRequest(AddTagRequest request)
        {

            if (request.DisplayName is not null&& request.DisplayName is not null)
            {
                return;
            }
            if (request.DisplayName==request.Name)
            {
                ModelState.AddModelError("DisplayName", "Name Cannot be same as DisplayName");
            }

        }


    }
}