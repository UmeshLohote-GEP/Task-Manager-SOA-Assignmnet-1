using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers
{


    public class TaskDbContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class TaskMaintainController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskMaintainController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpPost("/tasks")]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpGet("/tasks/{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("/tasks/{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpPost("/tasks/{id}/complete")]
        public IActionResult CompleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Status = "Completed";
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpDelete("/tasks/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("/tasks")]
        public IActionResult ListTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }
    }
}