﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubUsers.UnitTests
{
    public class FakeMessageHandler : HttpMessageHandler
    {

        private readonly Dictionary<Uri, HttpResponseMessage> _FakeResponses = new Dictionary<Uri, HttpResponseMessage>(); 

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage)
        {
                _FakeResponses.Add(uri,responseMessage);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_FakeResponses.ContainsKey(request.RequestUri))
            {
                return _FakeResponses[request.RequestUri];
            }
            
            return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request};            
        }
    }

}