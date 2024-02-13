using DockerTutorial.DLL;
using DockerTutorial.DLL.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerTutorial.BLL.Services
{
    public interface IContactService
    {
        Task<Contact> SaveContact(Contact aContact);

        Task<List<Contact>> GetAllContact();

        Task<Contact> GetAContact(int id);

        Task<Contact> UpdateContact(int id, Contact aContact);

        Task<Contact> DeleteContact(int id);
    }

    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context )
        {
            _context = context;
        }

       

        public async Task<Contact> SaveContact(Contact aContact)
        {
            await _context.Contacts.AddAsync(aContact);
            await _context.SaveChangesAsync();
            return aContact;
        }

        public async Task<List<Contact>> GetAllContact()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetAContact(int id)
        {
            var contact =  await _context.Contacts.FindAsync(id);

            if(contact == null)
            {
                throw new Exception("contact not found");
            }

            return contact;
            
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                throw new Exception("contact not found");
            }
            _context.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;

        }

        public async Task<Contact> UpdateContact(int id, Contact aContact)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Name = aContact.Name;
                contact.Phone = aContact.Phone;
                contact.Address = aContact.Address;
                contact.Email = aContact.Email;
                await _context.SaveChangesAsync();
                return contact;
            }
            throw new Exception("contact not found");
        }

        
    }
}
