using Project.Dto;

namespace Project.Interface
{
    public interface IQRScannerRepository
    {
       public Task <QRDto> getInfoByRoomCode (string roomCode);
    }
}
