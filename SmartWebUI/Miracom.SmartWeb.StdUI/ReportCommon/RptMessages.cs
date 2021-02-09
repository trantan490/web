using System;
using System.Collections.Generic;
using System.Text;

namespace Miracom.SmartWeb.UI
{
    public sealed class RptMessages
    {
        public static string GetMessage(string MsgNum, char LanguageCode)
        {            
            string Msg = null;

            switch (MsgNum)
            {
                    ///Information
                case "INF001":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Successfully completed."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 작업을 성공적으로 수행하였습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Công việc đã được thực hiện thành công."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O trabalho foi realizado com sucesso."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Successfully completed."; }
                    break;
                
                case "INF002":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Are you sure you want to delete the data?"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 데이타를 지우시겠습니까?"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - bạn chắc chắn muốn xóa dữ liệu?"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Tem certeza de que deseja excluir os dados?"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Are you sure you want to delete the data?"; }
                    break;
                
                case "INF003":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This command will UPDATE related data. Are you sure you want to execute the command?"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 명령을 수행하면 관련된 여러 데이타가 업데이트됩니다. 그래도 계속하시겠습니까?"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Nếu bạn chạy lệnh này thì nhiều dữ liệu liên quan sẽ được cập nhật. bạn có chắc bạn muốn tiếp tục không?"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Quando você executar este comando várias dados atualizações relacionadas. Tem certeza de que deseja continuar?"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This command will UPDATE related data. Are you sure you want to execute the command?"; }
                    break;
                    
                    ///Error
                case "STD000":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Fatal database error is occured. Please contact admin person."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Database 작업중 오류가 발생 하였습니다. 관리자에게 문의 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Cơ sở dữ liệu đang làm việc trên đã xảy ra lỗi. Vui lòng liên hệ với quản trị viên."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Banco de dados está trabalhando em que ocorreu o erro. Entre em contato com o administrador."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Fatal database error is occured. Please contact admin person."; }
                    break;

                case "STD001":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Factory Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Factory값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị nhà máy."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique os valores de fábrica."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Factory Code."; }
                    break;

                case "STD002":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Can not export(No Data)."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 데이터가 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có dữ liệu."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Não há dados."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Can not export(No Data)"; }
                    break;

                case "STD003":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Material Type."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Material Type값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị Loại vật liệu."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique o valor tipo de material."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Material Type."; }
                    break;

                case "STD004":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Group By."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Group By값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị theo nhóm."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite o Grupo por valor."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Group By."; }
                    break;

                case "STD005":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Operation Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Operation값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị hoạt động."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor insira os valores de operação."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Operation Code."; }
                    break;

                case "STD006":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Material Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Material값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị vật chất."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique o valor material."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Material Code."; }
                    break;

                case "STD007":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Material Group."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Material Group값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị nhóm vật chất."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor insira os valores de grupo de materiais."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Material Group."; }
                    break;

                case "STD008":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Resource Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Resource값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị tài nguyên."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite o valor do recurso."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " -  Please Input Resource Code."; }
                    break;

                case "STD009":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Lot ID."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Lot ID를 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các lot ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite o Lot ID."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Lot ID."; }
                    break;

                case "STD010":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Not Found Data."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - DATA가 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có dữ liệu."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Os dados não são."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Not Found Data."; }
                    break;

                case "STD011":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Material Version."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Material Version을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập phiên bản vật chất."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique a versão de materiais."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Material Version."; }
                    break;

                case "STD012":
                    if (LanguageCode == '1') { Msg = MsgNum + " - The data is duplicated."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 데이타가 다른 데이타와 중복됩니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Dữ liệu bị trùng lặp với dữ liệu khác."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Os dados são duplicados com outros dados."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - The data is duplicated."; }
                    break;

                case "STD013":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This User does not exist. Please check user id again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 사용자는 존재하지 않습니다. 사용자 ID를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Người dùng này không tồn tại. Vui lòng kiểm tra ID của bạn một lần nữa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Successfully completed."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Este usuário não existe. Por favor, verifique seu ID mais uma vez."; }
                    break;
                
                case "STD014":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This  Security Group does not exist. please check security group id again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 Security 그룹은 존재하지 않습니다. Security 그룹 ID를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Nhóm an ninh không tồn tại. Vui lòng kiểm tra một lần một ID nhóm an ninh."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O grupo de segurança não existe. Por favor, verifique uma vez por ID de grupo de segurança."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This  Security Group does not exist. please check security group id again."; }
                    break;

                case "STD015":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This User already exist. Please check user id again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 사용자는 이미 존재합니다. 사용자 ID를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Người dùng này đã tồn tại sẵn. Vui lòng kiểm tra ID của bạn một lần nữa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Este usuário já existe. Por favor, verifique seu ID mais uma vez."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This User already exist. Please check user id again."; }
                    break;

                case "STD016":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This  Security Group already exist. please check security group id again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 Security 그룹은 이미 존재합니다. Security 그룹 ID를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Nhóm an ninh này đã tồn tại sẵn. Vui lòng kiểm tra ID nhóm an ninh một lần nữa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O Grupo de Segurança já está presente. Por favor, verifique uma vez por ID de grupo de segurança."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This  Security Group already exist. please check security group id again."; }
                    break;

                case "STD017":
                    if (LanguageCode == '1') { Msg = MsgNum + " - You can not delete security group because of some users included in the security group."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - SECURITY GROUP에 속해있는 USER가 있어 SECURITY GROUOP을 삭제할 수 없읍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn không thể xóa nhóm an ninh vì có môtt người dùng thuộc nhóm an ninh này."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você não pode excluir SEGURANÇA Grouop tenho usuário pertencente ao grupo de segurança."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You can not delete security group because of some users included in the security group."; }
                    break;

                case "STD018":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This  Function does not exist. Please check Function Name again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 Function 존재하지 않습니다. Function Name를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chức năng này không tồn tại. Vui lòng kiểm tra lại một lần nữa tên chức năng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - A função não existe. Por favor, verifique novamente o nome da função."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This  Function does not exist. Please check Function Name again."; }
                    break;

                case "STD019":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This  Function already exist. please check Function Name again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 Function은 이미 존재합니다. Function Name을 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chức năng này đã tồn tại. Vui lòng kiểm tra lại một lần nữa tên chức năng này."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - A função já existe. Por favor, verifique novamente o nome da função."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This  Function already exist. please check Function Name again."; }
                    break;
                       
                case "STD020":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This Menu already exist. please check Menu again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 MENU는 이미 존재합니다. MENU를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - MENU này đã tồn tại. Vui lòng kiểm tra lại MENU một lần nữa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O MENU já existe. Verifique MENU mais uma vez."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This Menu already exist. please check Menu again."; }
                    break;

                case "STD021":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This Menu does not exist. please check Menu again."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 MENU는 존재하지 않습니다. MENU를 다시 한번 확인해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - MENU này không tồn tại. Vui lòng kiểm tra lại MENU một lần nữa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O menu não existe. Verifique MENU mais uma vez."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This Menu does not exist. please check Menu again."; }
                    break;

                case "STD022":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This Function alread attached to the menu."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 FUNCTION은 이미 MENU에 할당되어 있습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chức năng này đã được gắn sẵn cho MENU."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - FUNÇÃO Isso já foi atribuído ao MENU."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This Function alread attached to the menu."; }
                    break;

                case "STD023":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This Function does not  attached to the menu."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 FUNCTION은 MENU에 할당되어 있지 않습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chức năng không được gắn sẵn cho MENU."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - A função não é atribuída ao MENU."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This Function does not  attached to the menu."; }
                    break;

                case "STD024":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Area Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Area값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị Area."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique o valor da área."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Area Code."; }
                    break;

                case "STD025":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input sCodeColumnName and sValueColumnName."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - sCodeColumnName과 sValueColumnName을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập sCodeColumnName và sValueColumnName."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique sCodeColumnName e sValueColumnName."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input sCodeColumnName and sValueColumnName."; }
                    break;

                case "STD026":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Loss Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Loss Code값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị Loss Code."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique valor Loss Code."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Loss Code."; }
                    break;

                case "STD027":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Detail - Please Input Upeh Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Detail - Upeh값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Detail - Vui lòng nhập giá trị Upeh."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Detail - Por favor, indique valor Upeh."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + "- Detail - Please Input Upeh Code."; }
                    break;

                case "STD028":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Detail - Please Select RES"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Detail - 설비를 선택해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Detail - Vui lòng chọn thiết bị."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Detail - Por favor, selecione o equipamento."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Detail - Please Select RES"; }
                    break;

                case "STD029":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Select RefFactory Property."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Factory 참조컨트롤을 설정해 주세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng thiết lập bộ kiểm soát điều khiển nhà máy."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, consulte os controles de origem."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Select RefFactory Property."; }
                    break;

                case "STD030":
                    if (LanguageCode == '1') { Msg = MsgNum + " - No Equipmemts exists in specific period. Please modify searching period."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 해당기간내에 장비정보가 없을 수 있습니다. 검색기간을 재조정해보십시오."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn không thể có thông tin thiết bị trong khoảng thời gian tương ứng. Vui lòng điều chỉnh lại thời gian tìm kiếm."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você não tem as informações de equipamentos no período em apreço. Tente período de re-pesquisa."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - No Equipmemts exists in specific period. Please modify searching period."; }
                    break;

                case "STD031":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Can not Select FGS, HMKE1, Factory! Select Others."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - HMKE1, FGS 는 선택 할 수 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không thể chọn HMKE1, FGS."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - HMKE1, FGS não pode ser selecionado."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Can not Select FGS, HMKE1, Factory! Select Others."; }
                    break;
                
                //2010-01-28-임종우 : FLOW 값 에러 메세지 추가.
                case "STD032":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Flow Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Flow값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị Flow.."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique os valores de fluxo."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Flow Code."; }
                    break;

                //2010-02-09-임종우 : CUSTOMER 값 에러 메세지 추가.
                case "STD033":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input CUSTOMER Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - CUSTOMER값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị khách hàng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor insira os valores do cliente."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input CUSTOMER Code."; }
                    break;

                case "STD034":      //2010-09-02-정비재 : PACKAGE ERROR 추가
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Package Group."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Package Group값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập các giá trị Nhóm Package."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor insira os valores de grupo de pacotes."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Package Group."; }
                    break;

                case "STD035":      // 2020-01-31-김미경 : 언어 변경에 따른 재로그은 Message 추가
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please re-login to apply the changed language."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 변경된 언어로 적용하시려면 재로그인 해주시길 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng đăng nhập lại để áp dụng các ngôn ngữ đã được thay đổi."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Para receber Confirmação Imediata Conecte-se novamente para aplicar a linguagem mudou."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please re-login to apply the changed language."; }
                    break;

                case "STD036":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Data Binding error: A problem has occurred to link the data to check the combo box."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 데이타 바인딩 에러 : 체크 콤보 박스에 데이타를 연결하는데 문제가 발생하였습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Dữ liệu liên kết bị lỗi: Đã xảy ra vấn đề khi liên kết các dữ liệu trong hộp combo kiểm tra."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Ligação de dados de erro: Ocorreu um problema para vincular os dados para verificar a caixa de combinação."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Data Binding error: A problem has occurred to link the data to check the combo box."; }
                    break;

                case "STD037":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Invalid Binding button. [RPT_ColumnConfigFromTable]"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 잘못된 바인딩 버튼입니다. [RPT_ColumnConfigFromTable]."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Nút kết lối không hợp lệ. [RPT_ColumnConfigFromTable]"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - botão de vinculação inválido. [RPT_ColumnConfigFromTable]"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Invalid Binding button. [RPT_ColumnConfigFromTable]"; }
                    break;

                case "STD038":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your customer (Custom)."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 고객사(Custom)를 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập khách hàng của bạn (Thói quen)."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique o seu cliente (Custom)."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your customer (Custom)."; }
                    break;

                case "STD039":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Enter the Package."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Package를 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập Package."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite o pacote.."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Enter the Package."; }
                    break;

                case "STD040":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your search criteria LOT_ID."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - LOT_ID 검색 조건을 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập điều kiện tìm kiếm của LOT_ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite os seus critérios de busca LOT_ID."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your search criteria LOT_ID."; }
                    break;

                case "STD041":     
                    if (LanguageCode == '1') { Msg = MsgNum + " - LOT ID is required"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - LOT ID가 필요합니다"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Cần Có LOT ID"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - LOT ID é necessária"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - LOT ID is required"; }
                    break;

                case "STD042":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select the chart ID."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 차트 ID를 선택 하여 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn ID biểu đồ."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Selecione o ID gráfico."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please select the chart ID."; }
                    break;

                case "STD043":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select the character ID."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 캐릭터 ID를 선택 하여 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn ký tự ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Selecione o ID caráter."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please select the character ID."; }
                    break;

                case "STD044":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please choose the character or PKG ID information."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 캐릭터 ID 또는 PKG 정보를 선택 하여 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn thông tin PKG hoặc các ký tự ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, escolha o personagem ou informações PKG ID."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please choose the character or PKG ID information."; }
                    break;

                case "STD045":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - The only monthly / weekly views."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 월간 / 주간 조회만 가능합니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chỉ có khả năng kiểm tra hàng tháng / hàng tuần."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Apenas as opiniões semanais / mensais."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - The only monthly / weekly views."; }
                    break;

                case "STD046":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select a Chart"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Chart를 선택해 주세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn biểu đồ"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, selecione um gráfico"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please select a Chart"; }
                    break;

                case "STD047":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - You have selected too many characters."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 너무 많은 캐릭터를 선택 하셨습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn đã chọn quá nhiều ký tự"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite seu SPEC_ID"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You have selected too many characters."; }
                    break;

                case "STD048":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your SPEC_ID"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - SPEC_ID를 입력하세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập SPEC_ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Selecione o ID caráter."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your SPEC_ID"; }
                    break;

                case "STD049":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your CHAR_ID"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - CHAR_ID를 입력하세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập CHAR_ID của bạn"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite seu CHAR_ID"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your CHAR_ID"; }
                    break;

                case "STD050":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Select the process."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 공정을 선택하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn quy trình."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Selecione o processo."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Select the process."; }
                    break;

                case "STD051":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Choose the customer first,"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 고객사를 먼저 선택하세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn khách hàng trước tiên,"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Escolha o cliente em primeiro lugar."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Choose the customer first,"; }
                    break;

                case "STD052":      
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your HMKA1."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - HMKA1을 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập HMKA1 của bạn."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite seu HMKA1."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your HMKA1."; }
                    break;

                case "STD053":
                    if (LanguageCode == '1') { Msg = MsgNum + " - You have selected too many processes."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 너무 많은 공정을 선택 하셨습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn đã chọn quá nhiều quy trình."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você selecionou muitos processos."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You have selected too many processes."; }
                    break;

                case "STD054":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Factory can only select HMKA1."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Factory 는 HMKA1 만 선택할 수 있습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Nhà máy chỉ có thể chọn HMKA1."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Fábrica só pode selecionar HMKA1."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Factory can only select HMKA1."; }
                    break;

                case "STD055":
                    if (LanguageCode == '1') { Msg = MsgNum + " - No details of the corresponding Part."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 해당 하는 Part의 세부 정보가 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có thông tin chi tiết của phần tương ứng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Nenhum detalhe da parte correspondente."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - No details of the corresponding Part."; }
                    break;

                case "STD056":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please re-viewed short period."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 기간을 줄여서 다시 조회 하시기 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng hãy rút ngắn thời gian và tìm kiếm lại."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, re-visto curto período."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please re-viewed short period."; }
                    break;

                case "STD057":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Settlement data can be viewed only historical date."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 결산 데이터는 과거 일자만 조회가 가능합니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Dữ liệu quyết toán chỉ những ngày đã qua mới có khả năng kiểm tra."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - dados de liquidação pode ser visto apenas data histórica."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Settlement data can be viewed only historical date."; }
                    break;

                case "STD058":
                    if (LanguageCode == '1') { Msg = MsgNum + " - May 30, 2011 at 11:00 previous data does not exist."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 2011년 5월 30일 11시 이전 데이터는 존재 하지 않습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Lúc 11 giờ ngày 30 tháng 5 năm 2011 dữ liệu trước đó không tồn tại."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O 30 de maio, 2011 às 11:00 dados anteriores não existe."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - May 30, 2011 at 11:00 previous data does not exist."; }
                    break;

                case "STD059":
                    if (LanguageCode == '1') { Msg = MsgNum + " - FMB based on the facility LIST is shown only when viewed with the current standards."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - FMB 기준 해당 설비 LIST는 현재 기준으로 조회 할 경우에만 표시 됩니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Danh sách thiết bị dựa trên FMB tương ứng chỉ được hiển thị khi tìm kiếm trên cơ sở hiện tại.."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - FMB com base na lista instalação é mostrada apenas quando visto com os padrões atuais."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - FMB based on the facility LIST is shown only when viewed with the current standards."; }
                    break;

                case "STD060":
                    if (LanguageCode == '1') { Msg = MsgNum + " - TEST is the primary sales only query."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - TEST 는 기본과 매출만 조회 가능합니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - TEST chỉ có khả năng kiểm tra doanh số bán hàng và những cái cơ bản."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - TEST é o principal vendas única consulta."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - TEST is the primary sales only query."; }
                    break;

                case "STD061":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Enter the time, please enter the number 2 spot."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 시간 입력은 2자리 수로 입력 하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập thời gian bằng 2 chữ số."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Introduza a hora, deve inserir o ponto do número 2."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Enter the time, please enter the number 2 spot."; }
                    break;

                case "STD062":
                    if (LanguageCode == '1') { Msg = MsgNum + " - You can not enter more than 60 minutes."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 60분 이상 입력 하실 수 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn không thể nhập quá 60 phút."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você não pode inserir mais de 60 minutos."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You can not enter more than 60 minutes."; }
                    break;

                case "STD063":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select a Version."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Version 을 선택 하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn một phiên bản."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, selecione uma versão."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please select a Version."; }
                    break;

                case "STD064":
                    if (LanguageCode == '1') { Msg = MsgNum + " - You have selected too many processes."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 너무 많은 공정을 선택 하셨습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Bạn đã chọn quá nhiều quy trình."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você selecionou muitos processos."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You have selected too many processes."; }
                    break;

                case "STD065":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter the distinction"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 구분을 입력하세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập sự phân loại"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique a distinção"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter the distinction"; }
                    break;

                case "STD066":
                    if (LanguageCode == '1') { Msg = MsgNum + " - No equipment is assigned."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 할당되어 있는 설비가 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có thiết bị được gắn sẵn."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Nenhum equipamento é atribuído."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - No equipment is assigned."; }
                    break;

                case "STD067":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter only numeric performance efficiency."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 성능효율은 숫자만 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Hiệu suất của tính năng vui lòng chỉ nhập chữ số."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, digite apenas eficiência de desempenho numérico."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter only numeric performance efficiency."; }
                    break;

                case "STD068":
                    if (LanguageCode == '1') { Msg = MsgNum + " - During that period, there is no unregistered products."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 해당 기간 동안은 미등록 된 제품이 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có sản phẩm chưa được đăng ký trong khoảng thời gian thích ứng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Durante esse período, não há produtos não registrados."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - During that period, there is no unregistered products."; }
                    break;

                case "STD069":
                    if (LanguageCode == '1') { Msg = MsgNum + " - The search process is up to 10."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 공정은 최대 10개 까지 검색 됩니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Quá trình được phép tìm kiếm tối đa đến 10 lần."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O processo de pesquisa é de até 10."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - The search process is up to 10."; }
                    break;

                case "STD070":
                    if (LanguageCode == '1') { Msg = MsgNum + " - The search period has been exceeded. It will search for up to 12 months."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 검색기간이 초과 되었습니다. 최대 12개월 까지 검색 됩니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Đã vượt quá thời gian tìm kiếm. Được tìm kiếm tối đa đến 12 tháng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O período de pesquisa foi excedido. Ele irá procurar por até 12 meses."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - The search period has been exceeded. It will search for up to 12 months."; }
                    break;

                case "STD071":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Select the Block or equipment."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Block 또는 설비를 선택하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn block hoặc thiết bị."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Selecione o bloco ou equipamento."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Select the Block or equipment."; }
                    break;

                case "STD072":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Equipment can be selected up to a maximum of 30."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 설비는 최대 30대까지 선택 가능합니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Thiết bị có thể được lựa chọn tối đa lên đến 30 cái."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O equipamento pode ser selecionada até um máximo de 30."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Equipment can be selected up to a maximum of 30."; }
                    break;

                case "STD073":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your information process."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 공정 정보를 입력하시기 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập thông tin quy trình."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Digite seu processo de informação."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your information process."; }
                    break;

                case "STD074":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter a time."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 시간을 입력하시기 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập thời gian."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor insira um tempo."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter a time."; }
                    break;

                case "STD075":              //삭제된 Message라 판단됨
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select process information."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 공정정보를 선택하여 주십시오."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn thông tin quy trình."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, selecione informações do processo."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please select process information."; }
                    break;

                case "STD076":
                    if (LanguageCode == '1') { Msg = MsgNum + " - From the date it is greater than the To date."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - From 날짜가 To 날짜보다 큽니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Ngày bắt đầu lớn hơn ngày kết thúc."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - A partir da data em que é maior do que a data para."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - From the date it is greater than the To date."; }
                    break;

                case "STD077":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter your Product or LOT_ID search criteria."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Product 또는 LOT_ID 검색 조건을 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập điều kiện tìm kiếm sản phẩm hoặc LOT_ID."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique o seu produto ou LOT_ID critérios de pesquisa."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter your Product or LOT_ID search criteria."; }
                    break;

                case "STD078":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please enter numbers in YIELD Field."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - YIELD는 숫자만 입력하세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chỉ nhập số YIELD."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RENDIMENTO Introduza apenas números."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please enter only numbers in YIELD Field."; }
                    break;

                case "STD079":
                    if (LanguageCode == '1') { Msg = MsgNum + " - You may wish to view the material in a code display."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 자재코드가 표시 된 상태로 조회 하시기 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng kiểm tra trạng thái được biểu thị bằng mã số nguyên liệu."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Você pode desejar ver o material em uma exibição do código."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - You may wish to view the material in a code display."; }
                    break;

                case "STD080":
                    if (LanguageCode == '1') { Msg = MsgNum + " - MATCODE information on the Group is required."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Group 정보에 MATCODE 는 필수 입니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - MATCODE cần thiết cho thông tin nhóm.."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - MATCODE informações sobre o Grupo é necessária."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - MATCODE information on the Group is required."; }
                    break;

                case "STD081":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please select PACKAGE"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - PACKAGE를 선택하여 주세요"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn PACKAGE"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, selecione PACOTE"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " -Please select PACKAGE"; }
                    break;

                case "STD082":
                    if (LanguageCode == '1') { Msg = MsgNum + " - WB process do not enter the plant number, the slower the speed! Would you like to search without entering the equipment number?"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - WB공정은 설비번호를 입력하지 않으면 속도가 느립니다! 설비번호를 입력하지 않고 검색하시겠습니까?"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Tốc độ quy trình WB sẽ bị chậm nếu như không nhập mã số thiết bị! Bạn có muốn tìm kiếm mà không cần nhập mã số thiết bị?"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - processo WB não inserir o número de plantas, mais lenta a velocidade! Gostaria de pesquisa sem digitar o número do equipamento?"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - WB process do not enter the plant number, the slower the speed! Would you like to search without entering the equipment number?"; }
                    break;

                    //STD083 ~ STD100까지 예외 시 발생되는 Hard Coding된 출력 Message
                case "STD083":
                    if (LanguageCode == '1') { Msg = MsgNum + " - udcCUSFromToCondition: data are not binding."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - udcCUSFromToCondition : 데이타가 바인딩 되지 않았습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - udcCUSFromToCondition: Dữ liệu chưa được liên kết."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - udcCUSFromToCondition: dados não são vinculativos."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - udcCUSFromToCondition: data are not binding."; }
                    break;

                case "STD084":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Title X coordinate greater than the number of rooms. X coordinate of rooms:"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Title 수가 X 좌표 갯수 보다 많습니다. X 좌표 갯수 : "; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Số lượng Title lớn hơn số lượng tọa độ X. Số tọa độ X:"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Título X coordenar maior que o número de quartos. Coordenada X de quartos:"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Title X coordinate greater than the number of rooms. X coordinate of rooms:"; }
                    break;

                case "STD085":
                    if (LanguageCode == '1') { Msg = MsgNum + " - SeriseTitle number greater than the number series. Series Number:"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - SeriseTitle 수가 시리즈수 보다 많습니다. 시리즈수 : "; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Số SeriseTitle lớn hơn Số series. Số series:"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - SeriseTitle número maior que o número de série. Número de Série:"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - SeriseTitle number greater than the number series. Series Number:"; }
                    break;

                case "STD086":
                    if (LanguageCode == '1') { Msg = MsgNum + " - udcDurationDate: data are not binding."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - udcDurationDate : 데이타가 바인딩 되지 않았습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - udcDurationDate: Dữ liệu chưa được liên kết."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - udcDurationDate: dados não são vinculativos."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - udcDurationDate: data are not binding."; }
                    break;

                case "STD087":
                    if (LanguageCode == '1') { Msg = MsgNum + " - More than one year will not be searched."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 일 년 이상은 검색이 불가능합니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Không có khả năng tìm kiếm từ một năm trở lên."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Mais de um ano não serão pesquisados."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - More than one year will not be searched."; }
                    break;

                case "STD088":
                    if (LanguageCode == '1') { Msg = MsgNum + " - udcDurationDate: The type is not yet defined."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - udcDurationDate : 아직 정의 되지 않는 타입입니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - udcDurationDate: Loại chưa được định nghĩa."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - udcDurationDate: O tipo não está definido ainda."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - udcDurationDate: The type is not yet defined."; }
                    break;

                case "STD089":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_SetBlankFromZero: The column type is invalid"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_SetBlankFromZero : 컬럼 타입이 잘못되었습니다"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_SetBlankFromZero: Loại cột không hợp lệ"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_SetBlankFromZero: O tipo de coluna é inválido"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_SetBlankFromZero: The column type is invalid"; }
                    break;

                case "STD090":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_AddDynamicColumn: Incorrect size of headers and format"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_AddDynamicColumn : headers 와 format 의 크기가 틀립니다"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_AddDynamicColumn: kích thước không đúng tiêu đề và định dạng"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_AddDynamicColumn: tamanho incorreto de cabeçalhos e formato"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_AddDynamicColumn: Incorrect size of headers and format"; }
                    break;

                case "STD091":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_AddRowsColor: RowCount this can not be zero."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_AddRowsColor : RowCount이 0 일 수는 없습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_AddRowsColor: rowCount này không thể bằng 0."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_AddRowsColor: RowCount isso não pode ser zero."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_AddRowsColor: RowCount this can not be zero."; }
                    break;

                case "STD092":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_AddRowsColor: no more than the number of columns that fixSize exists."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_AddRowsColor : fixSize 가 존재하는 컬럼수보다 더 많습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_AddRowsColor: fixSize nhiều hơn so với số lượng cột đang tồn tại."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_AddRowsColor: não mais do que o número de colunas que FixSize existe."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_AddRowsColor: no more than the number of columns that fixSize exists."; }
                    break;

                case "STD093":
                    if (LanguageCode == '1') { Msg = MsgNum + " - The value of the format to fill the Column is not valid."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Column을 채울 값의 형식이 올바르지 않습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Hình thức của giá trị sẽ điền vào cột là không hợp lệ."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - O valor do formato para preencher a coluna não é válido."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - The value of the format to fill the Column is not valid."; }
                    break;

                case "STD094":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Column position is incorrect."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Column위치가 잘못 되었습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - vị trí cột không chính xác."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - posição da coluna está incorreto."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Column position is incorrect."; }
                    break;

                case "STD095":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_AddRangeFormula: megae parameter is incorrect."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_AddRangeFormula : 메개 변수가 잘못 되었습니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_AddRangeFormula: tham số megae không chính xác."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_AddRangeFormula: parâmetro megae está incorreto."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_AddRangeFormula: megae parameter is incorrect."; }
                    break;

                case "STD096":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_AddDataUsingColumns : "; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_AddDataUsingColumns : "; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_AddDataUsingColumns:"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_AddDataUsingColumns:"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_AddDataUsingColumns : "; }
                    break;

                case "STD097":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_PrePaint:"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_PrePaint:"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_PrePaint:"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_PrePaint:"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_PrePaint:"; }
                    break;

                case "STD098":
                    if (LanguageCode == '1') { Msg = MsgNum + " - RPT_MouseMove : "; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - RPT_MouseMove : "; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - RPT_MouseMove : "; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - RPT_MouseMove : "; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - RPT_MouseMove : "; }
                    break;

                case "STD099":
                    if (LanguageCode == '1') { Msg = MsgNum + " - GetJoupTime"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - GetJoupTime"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - GetJoupTime"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - GetJoupTime"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - GetJoupTime"; }
                    break;

                case "STD0100":
                    if (LanguageCode == '1') { Msg = MsgNum + " - strStep"; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - strStep"; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - strStep"; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - strStep"; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - strStep"; }
                    break;

                case "STD0101":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Only the equipment down time within the inquiry period is counted."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 조회기간 내 설비 Down시간만 계산됩니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Chỉ có thiết bị xuống thời gian trong thời gian yêu cầu được tính."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Somente o tempo de inatividade do equipamento dentro do período de consulta é contado."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Only the equipment down time within the inquiry period is counted."; }
                    break;

                case "STD999":
                    if (LanguageCode == '1') { Msg = MsgNum + " - This is required field. Please enter a valid value."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 이 항목은 필요한 필드 입니다. 입력 바랍니다."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Hạng mục này yêu cầu cần field. Vui lòng nhập."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Esta entrada é um campo obrigatório. Por favor, indique."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - This is required field. Please enter a valid value."; }
                    break;
                    
                case "RAS001":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Moment Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - 순간정지 코드를 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập mã số tạm dừng."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor, indique a parada instantânea código."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Moment Code."; }
                    break;
                    
                case "PQC001":
                    if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Oven Code."; }
                    else if (LanguageCode == '2') { Msg = MsgNum + " - Oven Code값을 입력해 주세요."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng nhập giá trị Oven Code."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Código forno Por favor, indique o valor."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Oven Code."; }
                    break;
					
				case "REG001":
					if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Plan Week."; }
					else if (LanguageCode == '2') { Msg = MsgNum + " - 계획주차를 선택하여 주십시오."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn kế hoạch của tuần."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Por favor seleccione um estacionamento plano."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Plan Week."; }
					break;

				case "REG002":
					if (LanguageCode == '1') { Msg = MsgNum + " - Please Input Plan Week Version."; }
					else if (LanguageCode == '2') { Msg = MsgNum + " - 계획주차 버전을 선택하여 주십시오."; }
                    else if (LanguageCode == '3') { Msg = MsgNum + " - Vui lòng chọn phiên bản kế hoạch của tuần ."; }
                    else if (LanguageCode == '4') { Msg = MsgNum + " - Planos para selecionar a versão do estacionamento."; }
                    else if (LanguageCode == '5') { Msg = MsgNum + " - Please Input Plan Week Version."; }
					break;

            }

            return Msg;
        }
    }
}
