﻿using EventMate.Data;
using Eventmate_Data.Entities;
using Eventmate_Common.Helpers;
using EventMate_Data.Entities;
using EventMate_Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMate_Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        private readonly AESHelper _AESHelper;
        public AuthRepository(DataContext context, AESHelper AESHelper)
        {
            _context = context;
            _AESHelper = AESHelper;
        }
        public async Task<User?> IsValidUser(string email, string password)
        {
            try
            {
    
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
         
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user; 
            }

            return null; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetRoleUser(string email)
        {
            try
            {
                if (_context.Users == null)
            {
                return string.Empty;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }

            var role = await _context.Role!.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);
            return role != null ? role.RoleName : string.Empty;
        }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
}

        public async Task<User?> Login_Google(string email, string googleId)
        {
            try
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email && u.GoogleId == googleId);
            if (user != null)
            {
                return user;
            }
            return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SignUp(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OTPAuthen> CreateOTP(string email, string password)
        {
            try
            {
                var otp = new OTPAuthen
                {
                    OTPCode = Commons.GenerateOTP(),
                    ExpireTime = DateTime.Now.AddMinutes(5),
                    Token = _AESHelper.Encrypt(email + "::" + password),
                    CreateAt = DateTime.Now,
                };
                await _context.OTPAuthens.AddAsync(otp);
                await _context.SaveChangesAsync();

                return otp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<OTPAuthen> CheckOTP(string OTPCode, string token)
        {
            try
            {
                var otp = _context.OTPAuthens.FirstOrDefault(x => x.OTPCode == OTPCode && x.Token == token);

                return otp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
