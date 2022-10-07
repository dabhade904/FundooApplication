using BusinessLayer.Interface;
using CommanLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL: NoteInterfaceBL
    {
        private readonly  NoteInterfaceRL noteInterfaceRL;
        public NoteBL(NoteInterfaceRL noteInterfaceRL)
        {
            this.noteInterfaceRL = noteInterfaceRL;
        }
        public NoteEntity AddNotes( long userId,Note note)
        {
            try
            {
                return noteInterfaceRL.AddNotes(userId,note);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
