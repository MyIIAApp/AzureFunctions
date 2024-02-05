using System;
using System.Collections.Generic;
using System.Text;

namespace IIABackend
{
    /// <summary>
    /// Admin Name response class
    /// </summary>
    public class AdminNameResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNameResponse"/> class.
        /// </summary>
        /// <param name="name">Name</param>
        public AdminNameResponse(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }
    }
}
