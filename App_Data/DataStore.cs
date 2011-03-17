/* Software License Agreement (BSD License)
 * 
 * Copyright (c) 2010-2011, Rustici Software, LLC
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the <organization> nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Rustici Software, LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace HostedDemoApp
{
    public class DataStore
    {
        private static String connStr = ConfigurationManager.ConnectionStrings["DemoAppSqlConnection"].ConnectionString;

        //----------- Learning -------------

        public static Boolean AddEmptyLearningRecord(String learningId)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("insert into Learning (learning_id) values (@learning_id);", conn);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result == 1;
        }

        public static Boolean SetLearningTitle(String learningId, String title)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("update Learning set title = @title where learning_id = @learning_id", conn);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result == 1;
        }

        public static Boolean SetLearningDescription(String learningId, String description)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("update Learning set description = @description where learning_id = @learning_id", conn);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result == 1;
        }

        public static Learning GetLearning(String learningId)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("select learning_id, title, description " +
                                            "from Learning where learning_id = @learning_id", conn);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) {
                conn.Close();
                return null;
            }
            Learning learning = GetLearningFromReader(reader);
            conn.Close();
            return learning;
        }

        protected static Learning GetLearningFromReader(SqlDataReader reader)
        {
            Learning learning = new Learning();
            learning.Id = ((Guid)reader["learning_id"]).ToString();
            learning.Title = (String)reader["title"];
            learning.Description = (String)reader["description"];
            return learning;
        }


        //-------- Invitation ---------

        public static Boolean AddInvitation(String invitationId, String learningId, String senderEmail, String senderName)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("insert into Invitation (invitation_id, learning_id, sender_email, sender_name) " +
                                            "values (@invitation_id, @learning_id, @sender_email, @sender_name)", conn);
            cmd.Parameters.AddWithValue("@invitation_id", invitationId);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            cmd.Parameters.AddWithValue("@sender_email", senderEmail);
            cmd.Parameters.AddWithValue("@sender_name", senderName);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result == 1;
        }

        public static Invitation GetInvitation(String invitationId)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("select invitation_id, learning_id, sender_email, sender_name " + 
                                            "from Invitation where invitation_id = @invitation_id", conn);
            cmd.Parameters.AddWithValue("@invitation_id", invitationId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) {
                conn.Close();
                return null;
            }
            Invitation invitation = GetInvitationFromReader(reader);
            conn.Close();
            return invitation;
        }

        public static Invitation GetInvitationByLearningId(String learningId)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("select invitation_id, learning_id, sender_email, sender_name " +
                                            "from Invitation where learning_id = @learning_id", conn);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) {
                conn.Close();
                return null;
            }
            Invitation invitation = GetInvitationFromReader(reader);
            conn.Close();
            return invitation;
        }

        protected static Invitation GetInvitationFromReader(SqlDataReader reader)
        {
            Invitation invitation = new Invitation();
            invitation.Id = ((Guid)reader["invitation_id"]).ToString();
            invitation.LearningId = ((Guid)reader["learning_id"]).ToString();
            invitation.SenderEmail = (String)reader["sender_email"];
            invitation.SenderName = (String)reader["sender_name"];
            return invitation;
        }


        //----------- Registration --------------

        public static Registration GetRegistration(String regId)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("select registration_id, learning_id, registration_email, last_activity_time " + 
                                            "from Registration where registration_id = @registration_id", conn);
            cmd.Parameters.AddWithValue("@registration_id", regId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) {
                conn.Close();
                return null;
            }
            Registration reg = GetRegistrationFromReader(reader);
            conn.Close();
            return reg;
        }

        protected static Registration GetRegistrationFromReader(SqlDataReader reader)
        {
            Registration reg = new Registration();
            reg.Id = ((Guid)reader["registration_id"]).ToString();
            reg.LearningId = ((Guid)reader["learning_id"]).ToString();
            reg.Email = (String)reader["registration_email"];
            reg.LastActivityTime = (DateTime)reader["last_activity_time"];
            return reg;
        }

        public static Boolean AddRegistration(String learningId, String registrationId, String registrationEmail)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("insert into Registration (registration_id, learning_id, registration_email) " +
                                            "values (@registration_id, @learning_id, @registration_email)", conn);
            cmd.Parameters.AddWithValue("@registration_id", registrationId);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            cmd.Parameters.AddWithValue("@registration_email", registrationEmail);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            if (result != 1) {
                return false;
            }
            return true;
        }

        public static Boolean UpdateLastActivityTime(String registrationId)
        {
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("update Registration set last_activity_time = getdate() where registration_id = @registration_id", conn);
            cmd.Parameters.AddWithValue("@registration_id", registrationId);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            if (result != 1) {
                return false;
            }
            return true;
        }

        public static String[] GetRegistrationIdsForLearning(String learningId)
        {
            SqlConnection conn = new SqlConnection(connStr);

            List<String> regIds = new List<String>();
            SqlCommand cmd = new SqlCommand("select registration_id from Registration where learning_id = @learning_id", conn);
            cmd.Parameters.AddWithValue("@learning_id", learningId);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                regIds.Add(((Guid)reader["registration_id"]).ToString());
            }
            conn.Close();
            return regIds.ToArray();
        }
    }
}
