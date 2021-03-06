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
using System.Collections.Generic;
using SoftwareForge.Common.Models;
using System.Linq;

namespace SoftwareForge.DbService
{
    public class MessageDao
    {
        /// <summary>
        /// The DatabaseContext
        /// </summary>
        private static readonly SoftwareForgeDbContext SoftwareForgeDbContext = new SoftwareForgeDbContext();
       
        public static Message AddMessage(Message message)
        {
            Message createdMessage = SoftwareForgeDbContext.Messages.Add(message);
            SoftwareForgeDbContext.SaveChanges();
            return createdMessage;
        }

        public static List<Message> GetMessagesOfUser(User user)
        {
            
            try
            {
                return SoftwareForgeDbContext.Messages.Where(m => m.ToUserId == user.Id).ToList();
            }
            catch
            {
                return new List<Message>();
            }
        }

        public static void AddMessageForAllProjectOwner(Message message, Guid projectGuid)
        {
            var userList = ProjectsDao.GetUsers(projectGuid).Where(m => m.UserRole == UserRole.ProjectOwner);
            foreach (var projectMember in userList)
            {
                Message m = new Message {FromUserId = message.Id, Text = message.Text, ToUserId = projectMember.User.Id};
                SoftwareForgeDbContext.Messages.Add(m);
            }
            SoftwareForgeDbContext.SaveChanges();
        }
    }
}
