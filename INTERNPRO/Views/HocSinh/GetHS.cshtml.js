function MyAct() {
    var modal = document.getElementById("myModal");
    var openModalButton = document.getElementById("openModalButton");

    // Bắt đầu bằng việc ẩn modal
    modal.style.display = "none";

    // Xử lý sự kiện bấm nút mở modal
    openModalButton.onclick = function () {
        modal.style.display = "block";
    }

    // Đóng modal khi người dùng bấm bất kỳ đâu ngoài modal
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }

}