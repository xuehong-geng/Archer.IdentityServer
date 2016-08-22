﻿/*
 * Copyright 2015 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IdentityServer3.WsFederation.Validation;
using System.Threading.Tasks;

namespace IdentityServer3.WsFederation.Services
{
    /// <summary>
    /// Allows inserting custom validation logic into sign in requests
    /// </summary>
    public interface ICustomWsFederationRequestValidator
    {
        /// <summary>
        /// Custom validation logic for the sign in request.
        /// </summary>
        /// <param name="request">The validated request.</param>
        /// <returns>The validation result</returns>
        Task<SignInValidationResult> ValidateSignInRequestAsync(SignInValidationResult request);
    }
}
