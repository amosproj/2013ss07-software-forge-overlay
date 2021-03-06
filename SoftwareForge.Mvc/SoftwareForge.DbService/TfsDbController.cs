/*
 * Copyright (c) 2013 by Denis Bach, Konstantin Tsysin, Taner Tunc, Marvin Kampf, Florian Wittmann
 *
 * This file is part of the Software Forge Overlay rating application.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public
 * License along with this program. If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SoftwareForge.DbService
{
    /// <summary>
    /// Initialise a database controller which realises a new server connection with. 
    /// SQL database.
    /// </summary>
    public class TfsDbController
    {
        private readonly Server _server;
        private const String DbPrefix = "Tfs_";

        /// <summary>
        /// Create a new TfsDbController
        /// </summary>
        /// <param name="connectionString">the connectionString to the database</param>
        public TfsDbController(String connectionString)
        {
            _server = new Server(new ServerConnection(new SqlConnection(connectionString)));
            _server.Refresh();
        }

        /// <summary>
        /// Function to remove the database connection.
        /// </summary>
        /// <param name="name"> Name of the database</param>
        public void RemoveDatabase(String name)
        {
            _server.KillDatabase(DbPrefix + name);
        }

    }
}
