using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/books")] // Attribute routing prefix
    public class BooksController : ApiController
    {
        
         private WebAppContext db = new WebAppContext();

        // GET: api/books
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBooks()
        
         {
            try
            {
                var books = db.Books.ToList();
                return Ok(books);
            }
            catch  (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/books/5 
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            try
            
            
             {
                var book = db.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/books
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Book))]
        //public IHttpActionResult PostBook(Book book)
        //{
        //    try
        //           {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        db.Books.Add(book);
        //        db.SaveChanges();

        //        return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);

        //        return Ok(book);    
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult PostBook(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Books.Add(book);
                db.SaveChanges();

                // Create a location URI for the newly created resource
                var locationUri = Url.Link("DefaultApi", new { id = book.Id });

                return Created(locationUri, book);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }





        // PUT: api/books/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != book.Id)
                {
                    return BadRequest();
                }

                db.Entry(book).State = EntityState.Modified;       
                db.SaveChanges();

                return Ok(book);
            }
             catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/books/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            try
            {
                var book = db.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }

                db.Books.Remove(book);
                db.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
