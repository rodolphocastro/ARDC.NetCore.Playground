﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ARDC.NetCore.Playground.API.Features.Samples
{
    /// <summary>
    /// Controller for Sample related actions.
    /// </summary>
    [Route("samples")]
    public class SampleController : ControllerBase
    {
        public SampleController()
        {

        }

        /// <summary>
        /// Obtem uma string de Hello World.
        /// </summary>
        [HttpGet("hello", Name = "Say Hi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult HelloWorld() => Ok("Hello World!");

        /// <summary>
        /// Obtem o atual momento.
        /// </summary>
        [HttpGet("now", Name = "What time is it")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime))]
        public IActionResult Now() => Ok(DateTime.Now);

        /// <summary>
        /// Ping pong.
        /// </summary>
        [Authorize]
        [HttpPost("ping")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Pong() => Ok("Pong!");

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI() => Ok(HttpContext.User.Identity.Name);
    }
}
