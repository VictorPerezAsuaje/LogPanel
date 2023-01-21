const ChangeActiveClient = (clickedClient) => {
    const clients = document.querySelectorAll(".client-item");
    [...clients].forEach(c => c.classList.remove("active"));
    clickedClient.classList.add("active");
    PollLogs(clickedClient);
}

const PollLogs = (clickedClient = null) => {
    const pollingAlert = document.getElementById("refreshDataNotification");
    pollingAlert.classList.add("active");

    let activeClient = clickedClient;

    if (clickedClient == null)
        activeClient = document.querySelector(".client-item.active");

    const nMensajes = activeClient.querySelector("[id^=nMensajesNuevos-]");
    const primeraCarga = nMensajes.getAttribute("data-primeraCarga");

    fetch("Exceptions/PollClientLogs?" + new URLSearchParams({
        clientSelected: activeClient.getAttribute("data-clientName")
    }))
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            const logs = document.getElementById("logs");
            logs.innerHTML = result;

            const nuevosLogs = logs.querySelectorAll("[data-log='true']").length;

            if (primeraCarga == 'true') {
                nMensajes.setAttribute("data-primeraCarga", false);
                nMensajes.setAttribute("data-nMensajesAntes", nuevosLogs);
            }

            const nMensajesAntes = nMensajes.getAttribute("data-nMensajesAntes");

            nMensajes.setAttribute("data-nMensajesAhora", nuevosLogs);
            const numeroMensajesAntiguos = nMensajes.textContent;
            const numeroNuevosLogs = Number(nuevosLogs) - Number(nMensajesAntes)
            nMensajes.textContent = numeroNuevosLogs;

            if (numeroNuevosLogs > numeroMensajesAntiguos) {
                console.log("Tienes notificaciones nuevas!")
            }
        });

    setTimeout(() => pollingAlert.classList.remove("active"), 1000);
}

document.addEventListener("DOMContentLoaded", () => {
    setInterval(PollLogs, 30000);
})