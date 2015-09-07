using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.Policy
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Entity Accesses.
    /// </summary>
    [RoutePrefix("api/v1.5/policy/entity-access")]
    public class EntityAccessController : ApiController
    {
        /// <summary>
        ///     The EntityAccess data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Policy.Data.EntityAccess EntityAccessContext;

        public EntityAccessController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.EntityAccessContext = new MixERP.Net.Schemas.Policy.Data.EntityAccess
            {
                Catalog = this.Catalog,
                LoginId = this.LoginId
            };
        }

        public long LoginId { get; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Catalog { get; }

        /// <summary>
        ///     Counts the number of entity accesses.
        /// </summary>
        /// <returns>Returns the count of the entity accesses.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        public long Count()
        {
            try
            {
                return this.EntityAccessContext.Count();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Returns an instance of entity access.
        /// </summary>
        /// <param name="entityAccessId">Enter EntityAccessId to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{entityAccessId}")]
        public MixERP.Net.Entities.Policy.EntityAccess Get(int entityAccessId)
        {
            try
            {
                return this.EntityAccessContext.Get(entityAccessId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 entity accesses on each page, sorted by the property EntityAccessId.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        public IEnumerable<MixERP.Net.Entities.Policy.EntityAccess> GetPagedResult()
        {
            try
            {
                return this.EntityAccessContext.GetPagedResult();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 entity accesses on each page, sorted by the property EntityAccessId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Policy.EntityAccess> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.EntityAccessContext.GetPagedResult(pageNumber);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 entity accesses on each page, sorted by the property EntityAccessId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Policy.EntityAccess> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.EntityAccessContext.GetWhere(pageNumber, f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Displayfields is a lightweight key/value collection of entity accesses.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of entity accesses.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.EntityAccessContext.GetDisplayFields();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds your instance of Account class.
        /// </summary>
        /// <param name="entityAccess">Your instance of entity accesses class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{entityAccess}")]
        public void Add(MixERP.Net.Entities.Policy.EntityAccess entityAccess)
        {
            if (entityAccess == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.EntityAccessContext.Add(entityAccess);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Edits existing record with your instance of Account class.
        /// </summary>
        /// <param name="entityAccess">Your instance of Account class to edit.</param>
        /// <param name="entityAccessId">Enter the value for EntityAccessId in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{entityAccessId}/{entityAccess}")]
        public void Edit(int entityAccessId, MixERP.Net.Entities.Policy.EntityAccess entityAccess)
        {
            if (entityAccess == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.EntityAccessContext.Update(entityAccess, entityAccessId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Deletes an existing instance of Account class via EntityAccessId.
        /// </summary>
        /// <param name="entityAccessId">Enter the value for EntityAccessId in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{entityAccessId}")]
        public void Delete(int entityAccessId)
        {
            try
            {
                this.EntityAccessContext.Delete(entityAccessId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


    }
}