// <copyright file="IndexViewModel.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models
{
    /// <summary>
    /// IndexViewModel that shows TeamMembers.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets the list of TeamMembers' name.
        /// </summary>
        public IList<string> TeamMembers { get; set; }
    }
}
