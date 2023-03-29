using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RiversideFishhutDbContext _context;

        public RolesController(RiversideFishhutDbContext context)
        {
            _context = context;
        }

		// GET: api/Roles
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetRoles()
		{
			try
			{
				var roles = await _context.roles.ToListAsync();

				var result = roles.Select(r => new
				{
					r.RoleId,
					r.RoleName,
					RoleDescription = r.RoleDescription 
				}).ToList();

				return new CustomResponse(200, "Role list retrieved successfully", result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		[HttpPost]
		public async Task<ActionResult<object>> CreateRole(RoleCreateRequest request)
		{
			try
			{
				Role newRole = new Role
				{
					RoleName = request.RoleName,
					RoleDescription = request.Description
				};

				_context.roles.Add(newRole);
				await _context.SaveChangesAsync();

				var response = new
				{
					Status = 201,
					Message = "Role created successfully",
					Data = new
					{
						newRole.RoleId,
						RoleName = newRole.RoleName,
						RoleDescription = newRole.RoleDescription
					}
				};

				return StatusCode(201, response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}


		[HttpPut("{id}")]
		public async Task<ActionResult<object>> UpdateRole(int id, UpdateRoleRequest request)
		{
			try
			{
				Role roleToUpdate = await _context.roles.FindAsync(id);

				if (roleToUpdate == null)
				{
					return NotFound(new CustomResponse(404, "Role not found", null));
				}

				roleToUpdate.RoleName = request.RoleName;
				roleToUpdate.RoleDescription = request.Description;

				await _context.SaveChangesAsync();

				var response = new
				{
					Status = 200,
					Message = "Role updated successfully",
					Data = new
					{
						roleToUpdate.RoleId,
						RoleName = roleToUpdate.RoleName,
						RoleDescription = roleToUpdate.RoleDescription
					}
				};

				return StatusCode(200, response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteRole(int id)
		{
			try
			{
				Role roleToDelete = await _context.roles.FindAsync(id);

				if (roleToDelete == null)
				{
					return NotFound(new CustomResponse(404, "Role not found", null));
				}

				_context.roles.Remove(roleToDelete);
				await _context.SaveChangesAsync();

				return new CustomResponse(200, "Role deleted successfully", null);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}


	}
}
