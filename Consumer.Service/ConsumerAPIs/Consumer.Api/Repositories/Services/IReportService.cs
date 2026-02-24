using Consumer.Shared.DTOs.ResponseDTOs;

namespace Consumer.Api.Repositories.Services
{
    public interface IReportService
    {
        public Task<ResponseDto> GetAllReportsAsync();

        public Task<ResponseDto> GetReportByIdAsync(Guid id);

        //public Task<ResponseDto> AddReportAsync(ReportDto reportDto);
    }
}
