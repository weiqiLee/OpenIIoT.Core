﻿/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄████████
      █     ███    ███
      █     ███    █▀     ▄█████  ▄██████ ██   █     █████  █      ██    ▄█   ▄
      █     ███          ██   █  ██    ██ ██   ██   ██  ██ ██  ▀███████▄ ██   █▄
      █   ▀███████████  ▄██▄▄    ██    ▀  ██   ██  ▄██▄▄█▀ ██▌     ██  ▀ ▀▀▀▀▀██
      █            ███ ▀▀██▀▀    ██    ▄  ██   ██ ▀███████ ██      ██    ▄█   ██
      █      ▄█    ███   ██   █  ██    ██ ██   ██   ██  ██ ██      ██    ██   ██
      █    ▄████████▀    ███████ ██████▀  ██████    ██  ██ █      ▄██▀    █████
      █
      █   ▄████████
      █   ███    ███
      █   ███    █▀   ██████  ██▄▄▄▄      ██       █████  ██████   █        █          ▄█████    █████
      █   ███        ██    ██ ██▀▀▀█▄ ▀███████▄   ██  ██ ██    ██ ██       ██         ██   █    ██  ██
      █   ███        ██    ██ ██   ██     ██  ▀  ▄██▄▄█▀ ██    ██ ██       ██        ▄██▄▄     ▄██▄▄█▀
      █   ███    █▄  ██    ██ ██   ██     ██    ▀███████ ██    ██ ██       ██       ▀▀██▀▀    ▀███████
      █   ███    ███ ██    ██ ██   ██     ██      ██  ██ ██    ██ ██▌    ▄ ██▌    ▄   ██   █    ██  ██
      █   ████████▀   ██████   █   █     ▄██▀     ██  ██  ██████  ████▄▄██ ████▄▄██   ███████   ██  ██
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  WebApi Controller for the Security namespace.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  This program is free software: you can redistribute it and/or modify
      █  it under the terms of the GNU Affero General Public License as published by
      █  the Free Software Foundation, either version 3 of the License, or
      █  (at your option) any later version.
      █
      █  This program is distributed in the hope that it will be useful,
      █  but WITHOUT ANY WARRANTY; without even the implied warranty of
      █  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
      █  GNU Affero General Public License for more details.
      █
      █  You should have received a copy of the GNU Affero General Public License
      █  along with this program.  If not, see <http://www.gnu.org/licenses/>.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using OpenIIoT.Core.Service.WebApi;
using OpenIIoT.SDK;
using OpenIIoT.SDK.Common;
using OpenIIoT.SDK.Security;
using Swashbuckle.Swagger.Annotations;
using Utility.OperationResult;

namespace OpenIIoT.Core.Security.WebApi
{
    /// <summary>
    ///     WebAPI Controller for the Security namespace.
    /// </summary>
    [Authorize]
    [WebApiRoutePrefix("v1/security")]
    public class SecurityController : ApiBaseController
    {
        #region Protected Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public SecurityController()
            : base(ApplicationManager.GetInstance())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SecurityController"/> class with the specified
        ///     <see cref="IApplicationManager"/> instance.
        /// </summary>
        /// <param name="manager">The IApplicationManager instance used to resolve dependencies.</param>
        public SecurityController(IApplicationManager manager)
            : base(manager)
        {
        }

        #endregion Protected Constructors

        #region Private Properties

        /// <summary>
        ///     Gets the ISecurityManager for the application.
        /// </summary>
        private ISecurityManager SecurityManager => Manager.GetManager<ISecurityManager>();

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        ///     Gets the list of built-in User Roles.
        /// </summary>
        /// <returns>An HTTP response message.</returns>
        [HttpGet]
        [Route("roles")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, "The list was retrieved successfully.", typeof(IReadOnlyList<Role>))]
        public HttpResponseMessage RolesGet()
        {
            return Request.CreateResponse(HttpStatusCode.OK, SecurityManager.Roles, JsonFormatter());
        }

        /// <summary>
        ///     Ends the current Session.
        /// </summary>
        /// <returns>An HTTP response message.</returns>
        [HttpDelete]
        [Route("sessions/current")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, "The Session was successfully ended.")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "The Session could not be ended.", typeof(Result))]
        public HttpResponseMessage SessionsEnd()
        {
            HttpResponseMessage retVal;

            // coalesce the api key so that this method can be run under test. at runtime the Owin pipeline will not allow this
            // method to execute if the hash claim can't be satisfied, so additional checking for validity is not necessary.
            string apiKey = Request.GetOwinContext()?
                .Authentication?.User?.Claims?
                    .Where(c => c.Type == ClaimTypes.Hash).FirstOrDefault().Value ?? string.Empty;

            Session session = SecurityManager.FindSession(apiKey);

            IResult endSessionResult = SecurityManager.EndSession(session);

            if (endSessionResult.ResultCode != ResultCode.Failure)
            {
                retVal = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                retVal = Request.CreateResponse(HttpStatusCode.Unauthorized, endSessionResult, JsonFormatter());
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the list of active Sessions.
        /// </summary>
        /// <returns>An HTTP response message.</returns>
        [HttpGet]
        [Route("sessions")]
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(HttpStatusCode.OK, "The list was retrieved successfully.", typeof(IReadOnlyList<Session>))]
        public HttpResponseMessage SessionsGet()
        {
            return Request.CreateResponse(HttpStatusCode.OK, SecurityManager.Sessions, JsonFormatter(ContractResolverType.OptOut, "Subject"));
        }

        /// <summary>
        ///     Starts a new Session.
        /// </summary>
        /// <param name="userName">The user for which the Session is to be started.</param>
        /// <param name="password">The password with which to authenticate the user.</param>
        /// <returns>An HTTP response message.</returns>
        [HttpPost]
        [Route("sessions")]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "The Session was successfully started.", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "One or more parameters are invalid.", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "The Session could not be started.", typeof(Result))]
        public HttpResponseMessage SessionsStart(string userName, string password)
        {
            HttpResponseMessage retVal;

            if (string.IsNullOrEmpty(userName))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified User name is null or empty.");
            }
            else if (string.IsNullOrEmpty(password))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified password is null or empty.");
            }
            else
            {
                IResult<Session> startSessionResult = SecurityManager.StartSession(userName, password);

                if (startSessionResult.ResultCode != ResultCode.Failure)
                {
                    retVal = Request.CreateResponse(HttpStatusCode.OK, startSessionResult.ReturnValue, JsonFormatter(ContractResolverType.OptOut, "Subject"));
                }
                else
                {
                    IResult result = (Result)startSessionResult;
                    retVal = Request.CreateResponse(HttpStatusCode.Unauthorized, result, JsonFormatter());
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Creates a new User.
        /// </summary>
        /// <param name="name">The name of the new User.</param>
        /// <param name="password">The plaintext password for the new User.</param>
        /// <param name="role">The Role for the new User.</param>
        /// <returns>An HTTP response message.</returns>
        [HttpPost]
        [Route("users")]
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(HttpStatusCode.Created, "The User was created.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "One or more parameters are invalid.", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Conflict, "The specified User already exists.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "An unexpected error was encountered during the operation.", typeof(Result))]
        public HttpResponseMessage UsersCreate(string name, string password, Role role)
        {
            HttpResponseMessage retVal;

            if (string.IsNullOrEmpty(name))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified name is null or empty.");
            }
            else if (string.IsNullOrEmpty(password))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified password is null or empty.");
            }
            else
            {
                User user = SecurityManager.FindUser(name);

                if (user == default(User))
                {
                    IResult<User> createResult = SecurityManager.CreateUser(name, password, role);

                    if (createResult.ResultCode != ResultCode.Failure)
                    {
                        retVal = Request.CreateResponse(HttpStatusCode.Created, createResult.ReturnValue, JsonFormatter(ContractResolverType.OptOut, "PasswordHash"));
                    }
                    else
                    {
                        IResult result = (Result)createResult;
                        retVal = Request.CreateResponse(HttpStatusCode.InternalServerError, result, JsonFormatter());
                    }
                }
                else
                {
                    retVal = Request.CreateResponse(HttpStatusCode.Conflict, "The specified User already exists.");
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Deletes the specified User.
        /// </summary>
        /// <param name="name">The name of the User to delete.</param>
        /// <returns>An HTTP response message.</returns>
        [HttpDelete]
        [Route("users/{name}")]
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(HttpStatusCode.NoContent, "The User was deleted.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "One or more parameters are invalid.", typeof(string))]
        [SwaggerResponse(HttpStatusCode.NotFound, "The User does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "An unexpected error was encountered during the operation.", typeof(Result))]
        public HttpResponseMessage UsersDelete(string name)
        {
            HttpResponseMessage retVal;

            if (string.IsNullOrEmpty(name))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified name is null or empty.");
            }
            else
            {
                User user = SecurityManager.FindUser(name);

                if (user != default(User))
                {
                    IResult deleteResult = SecurityManager.DeleteUser(user.Name);

                    if (deleteResult.ResultCode != ResultCode.Failure)
                    {
                        retVal = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        retVal = Request.CreateResponse(HttpStatusCode.InternalServerError, deleteResult, JsonFormatter());
                    }
                }
                else
                {
                    retVal = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the list of configured Users.
        /// </summary>
        /// <returns>An HTTP response message.</returns>
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "Administrator")]
        [SwaggerResponse(HttpStatusCode.OK, "The list was retrieved successfully.", typeof(IReadOnlyList<User>))]
        public HttpResponseMessage UsersGet()
        {
            return Request.CreateResponse(HttpStatusCode.OK, SecurityManager.Users, JsonFormatter(ContractResolverType.OptOut, "PasswordHash"));
        }

        /// <summary>
        ///     Updates the specified User.
        /// </summary>
        /// <param name="name">The name of the User to update.</param>
        /// <param name="password">The updated plaintext password for the User.</param>
        /// <param name="role">The updated Role for the user.</param>
        /// <returns>An HTTP response message.</returns>
        [HttpPut]
        [Route("users/{name}")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, "The User was updated.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "One or more parameters are invalid.", typeof(string))]
        [SwaggerResponse(HttpStatusCode.NotFound, "The User does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "An unexpected error was encountered during the operation.", typeof(Result))]
        public HttpResponseMessage UsersUpdate(string name, string password = null, Role? role = null)
        {
            HttpResponseMessage retVal;

            if (string.IsNullOrEmpty(name))
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified name is null or empty.");
            }
            else if (password != null && password == string.Empty)
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "The specified password is empty.");
            }
            else if (password == null && role == null)
            {
                retVal = Request.CreateResponse(HttpStatusCode.BadRequest, "Neither the password nor the Role was specified; nothing to update.");
            }
            else
            {
                User user = SecurityManager.FindUser(name);

                if (user != default(User))
                {
                    IResult<User> updateResult = SecurityManager.UpdateUser(user.Name, password, role);

                    if (updateResult.ResultCode != ResultCode.Failure)
                    {
                        retVal = Request.CreateResponse(HttpStatusCode.OK, updateResult, JsonFormatter(ContractResolverType.OptOut, "PasswordHash"));
                    }
                    else
                    {
                        Result result = (Result)updateResult;
                        retVal = Request.CreateResponse(HttpStatusCode.InternalServerError, updateResult, JsonFormatter());
                    }
                }
                else
                {
                    retVal = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return retVal;
        }

        #endregion Public Methods
    }
}