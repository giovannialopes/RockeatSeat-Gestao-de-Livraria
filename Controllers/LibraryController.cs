using LibraryAPI.Class;
using LibraryAPI.Communication.Request;
using LibraryAPI.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

public static class DataStore
{
    public static List<RequestBookJSON> Informacoes { get; set; } = new List<RequestBookJSON>();
}

public class LibraryController : LibraryAPIBaseController
{

    [HttpGet("ID")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public IActionResult GetByID([FromHeader] int id)
    {
        foreach (var item in DataStore.Informacoes)
        {
            if (item.Id == id)
            {
                return Ok(item);
            }

        }
        return NoContent();
    }


    [HttpGet("allID")]
    public IActionResult GetByAll()
    {

        foreach (var item in DataStore.Informacoes)
        {
            if (item.Id != null)
            {
                return Ok(DataStore.Informacoes);
            }

        }
        return NoContent();
    }


    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status204NoContent)]
    public IActionResult GetByPost([FromBody] RequestBookJSON request)
    {
        DataStore.Informacoes.Add(request);
        return Created("",request);
    }


    [HttpPut("ID")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status204NoContent)]
    public IActionResult Update([FromHeader] int id, [FromBody] RequestBookJSON request)
    {
        var item = DataStore.Informacoes.FirstOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        item.Id = request.Id;
        item.Title = request.Title;
        item.Author = request.Author;
        item.filmGender = request.filmGender;  
        item.Price = request.Price;
        item.amountPrice = request.amountPrice; 

        return Ok(item);
    }


    [HttpDelete("ID")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status204NoContent)]
    public IActionResult Deleted([FromHeader] int id)
    {
        foreach (var item in DataStore.Informacoes)
        {
            if (item.Id == id)
            {
                DataStore.Informacoes.Remove(item);
                return NoContent();
            }

        }
        return NotFound();

    }

}
