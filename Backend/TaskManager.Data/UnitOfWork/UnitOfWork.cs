using System.Threading.Tasks;
using TaskManager.Data.Repositories;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Data.UnitOfWork.Interfaces;

namespace TaskManager.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;

        public ISubTaskRepository SubTaskRepository
        {
            get
            {
                if (SubTaskRepository is null)
                {
                    return new SubTaskRepository(context);
                }
                return SubTaskRepository;
            }
        }
        public IUserRepository UserRepository { get { return new UserRepository(context); } }
        public IToDoTaskRepository ToDoTaskRepository { get { return new ToDoTaskRepository(context); } }
        public ITaskFolderRepository TaskFolderRepository { get { return new TaskFolderRepository(context); } }


        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
