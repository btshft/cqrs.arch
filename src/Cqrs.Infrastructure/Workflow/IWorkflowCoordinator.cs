namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Маркерный интерфейс для регистрации координатора процесса.
    /// </summary>
    public interface IWorkflowCoordinator<TWorkflow> 
        where TWorkflow : IWorkflow { }
}