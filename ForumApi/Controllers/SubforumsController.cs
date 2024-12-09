﻿using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Posts.Output;
using Models.DTOs.Subforums.Input;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubforumsController : ControllerBase
    {
        private readonly ISubforumManager subforumManager;

        public SubforumsController(ISubforumManager subforumManager)
        {
            this.subforumManager = subforumManager;
        }


        [HttpGet]
        [Route("all-names")]
        public async Task<IActionResult> GetAllNames()
        {
            var names = await subforumManager.GetAllSubforumNamesAsync();

            return Ok(names);
        }

        [HttpGet]
        [Route("all-for-dropdown")]
        public async Task<IActionResult> GetAllForDropdown()
        {
            var subforums = await subforumManager.GetSubforumsForDropdownAsync();

            return Ok(subforums);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetSubforumByNameAsync([FromRoute] string name)
        {
            var operationResult = await subforumManager.GetSubforumByNameAsync(name, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSubforum(SubforumCreateDto subforumCreateDto)
        {
            var operationResult = await subforumManager.CreateSubforumAsync(User.GetId(), subforumCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(new { Name = operationResult.Data });
        }

        [HttpPost]
        [Route("join/{id}")]
        public async Task<IActionResult> JoinSubforum([FromRoute] long id)
        {
            var operationResult = await subforumManager.JoinSubforumAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("leave/{id}")]
        public async Task<IActionResult> LeaveSubforum([FromRoute] long id)
        {
            var operationResult = await subforumManager.LeaveSubforumAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
