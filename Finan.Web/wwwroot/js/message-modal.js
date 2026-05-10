window.showMessageModal = function (id, title, messages, type = "info") {

    // Modifica título
    document.querySelector(`#${id} .modal-title`).textContent = title;

    // Modifica cor do header
    const header = document.querySelector(`#${id} .modal-header`);
    header.className = `modal-header bg-${type} text-white`;

    // Modifica mensagens
    const bodyList = document.querySelector(`#${id}-body`);
    bodyList.innerHTML = "";

    if (Array.isArray(messages)) {
        messages.forEach(msg => {
            const li = document.createElement("li");
            li.textContent = msg;
            bodyList.appendChild(li);
        });
    } else {
        bodyList.innerHTML = `<li>${messages}</li>`;
    }

    // Abre modal
    const modal = new bootstrap.Modal(document.getElementById(id));
    modal.show();
}
