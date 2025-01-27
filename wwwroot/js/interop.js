// Inclure Chart.js via un CDN

// Fonction pour afficher une alerte
function createAlerte() {
    alert('Welcome to Levanti!');
}

// Fonction pour générer un graphique avec Chart.js
let chartInstance = null;

function GenerateBars(data, labels) {
    const ctx = document.getElementById("testchart").getContext("2d");

    // Détruire l'ancien graphique si existant
    if (chartInstance) {
        chartInstance.destroy();
    }

    // Créer un nouveau graphique
    chartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Coupons of Bond',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function GenerateBarsDuration(data, labels) {
    const ctx = document.getElementById("DurationChart").getContext("2d");

    // Détruire l'ancien graphique si existant
    if (chartInstance) {
        chartInstance.destroy();
    }

    // Créer un nouveau graphique
    chartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Duration',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}



function initializeDataTable(json) {
    // Parse le JSON reçu pour obtenir les données sous forme d'objet JavaScript
    const data = JSON.parse(json);

    // Exemple simple : Log des données
    console.log("Données reçues :", data);

    // Si tu veux générer un tableau HTML dynamiquement :
    const table = document.getElementById("dataTable");

    // Efface le contenu précédent du tableau
    table.innerHTML = "";

    // Ajoute les en-têtes
    if (data.length > 0) {
        const thead = table.createTHead();
        const headerRow = thead.insertRow();

        Object.keys(data[0]).forEach((key) => {
            const th = document.createElement("th");
            th.textContent = key;
            headerRow.appendChild(th);
        });
    }

    // Ajoute les données
    const tbody = table.createTBody();

    data.forEach((row) => {
        const tr = tbody.insertRow();

        Object.values(row).forEach((value) => {
            const td = tr.insertCell();
            td.textContent = value !== null ? value : ""; // Gestion des valeurs nulles
        });
    });

    // Initialisation de DataTables.js avec $(document).ready() pour s'assurer que le DOM est prêt

}





function initializeDataTable1(json) {
    // Parse le JSON reçu pour obtenir les données sous forme d'objet JavaScript
    const data = JSON.parse(json);

    // Exemple simple : Log des données
    console.log("Données reçues :", data);

    // Si tu veux générer un tableau HTML dynamiquement :
    const table = document.getElementById("dataTable1");

    // Efface le contenu précédent du tableau
    table.innerHTML = "";

    // Ajoute les en-têtes
    if (data.length > 0) {
        const thead = table.createTHead();
        const headerRow = thead.insertRow();

        Object.keys(data[0]).forEach((key) => {
            const th = document.createElement("th");
            th.textContent = key;
            headerRow.appendChild(th);
        });
    }

    // Ajoute les données
    const tbody = table.createTBody();

    data.forEach((row) => {
        const tr = tbody.insertRow();

        Object.values(row).forEach((value) => {
            const td = tr.insertCell();
            td.textContent = value !== null ? value : ""; // Gestion des valeurs nulles
        });
    });

    // Si DataTable existe déjà, on le détruit avant de réinitialiser
    if ($.fn.dataTable.isDataTable('#dataTable1')) {
        $('#dataTable1').DataTable().clear().destroy();
    }

    // Initialisation de DataTables.js avec $(document).ready() pour s'assurer que le DOM est prêt
    $(document).ready(function () {
        if ($.fn.DataTable) {
            $(table).DataTable({
                paging: true, // Activer la pagination
                searching: true, // Activer le champ de recherche
                ordering: true, // Activer le tri
                pageLength: 10, // Nombre de lignes affichées par page
                lengthMenu: [5, 10, 25, 50, 100], // Options pour le nombre de lignes par page
                language: {
                    search: "Rechercher :", // Texte pour le champ de recherche
                    lengthMenu: "Afficher _MENU_ enregistrements par page",
                    info: "Affichage de _START_ à _END_ sur _TOTAL_ enregistrements",
                    paginate: {
                        first: "Première",
                        last: "Dernière",
                        next: "Suivant",
                        previous: "Précédent",
                    },
                },
            });
        } else {
            console.error("DataTables.js n'est pas chargé. Assurez-vous que la bibliothèque est incluse.");
        }
    });
}

function resetDataTable1() {
    // Vérifie si la DataTable existe déjà
    if ($.fn.dataTable.isDataTable('#dataTable1')) {
        $('#dataTable1').DataTable().clear().destroy(); // Supprime les anciennes données
    }
    // Réinitialise le tableau pour qu'il reste vide
    $('#dataTable1').empty();
}

function resetDataTable() {
    if ($.fn.dataTable.isDataTable('#dataTable')) {
        $('#dataTable').DataTable().clear().destroy();
    }
    $('#dataTable').empty();
}




function resetDataRate() {
    if ($.fn.dataTable.isDataTable('#dataTableRate')) {
        $('#dataTableRate').DataTable().clear().destroy();
    }
    $('#dataTableRate').empty();
}



function initializeDataRate(json) {
    // Parse le JSON reçu pour obtenir les données sous forme d'objet JavaScript
    const data = JSON.parse(json);

    // Exemple simple : Log des données
    console.log("Données reçues :", data);

    // Si tu veux générer un tableau HTML dynamiquement :
    const table = document.getElementById("dataTableRate");

    // Efface le contenu précédent du tableau
    table.innerHTML = "";

    // Ajoute les en-têtes
    if (data.length > 0) {
        const thead = table.createTHead();
        const headerRow = thead.insertRow();

        Object.keys(data[0]).forEach((key) => {
            const th = document.createElement("th");
            th.textContent = key;
            headerRow.appendChild(th);
        });
    }

    // Ajoute les données
    const tbody = table.createTBody();

    data.forEach((row) => {
        const tr = tbody.insertRow();

        Object.values(row).forEach((value) => {
            const td = tr.insertCell();
            td.textContent = value !== null ? value : ""; // Gestion des valeurs nulles
        });
    });

    // Si DataTable existe déjà, on le détruit avant de réinitialiser
    if ($.fn.dataTable.isDataTable('#dataTableRate')) {
        $('#dataTableRate').DataTable().clear().destroy();
    }

    // Initialisation de DataTables.js avec $(document).ready() pour s'assurer que le DOM est prêt
    $(document).ready(function () {
        if ($.fn.DataTable) {
            $(table).DataTable({
                paging: true, // Activer la pagination
                searching: true, // Activer le champ de recherche
                ordering: true, // Activer le tri
                pageLength: 10, // Nombre de lignes affichées par page
                lengthMenu: [5, 10, 25, 50, 100], // Options pour le nombre de lignes par page
                language: {
                    search: "Rechercher :", // Texte pour le champ de recherche
                    lengthMenu: "Afficher _MENU_ enregistrements par page",
                    info: "Affichage de _START_ à _END_ sur _TOTAL_ enregistrements",
                    paginate: {
                        first: "Première",
                        last: "Dernière",
                        next: "Suivant",
                        previous: "Précédent",
                    },
                },
            });
        } else {
            console.error("DataTables.js n'est pas chargé. Assurez-vous que la bibliothèque est incluse.");
        }
    });
}


function GenerateLineRate(data, labels) {
    const ctx = document.getElementById("RateChart").getContext("2d");

    // Détruire l'ancien graphique si existant
    if (chartInstance) {
        chartInstance.destroy();
    }

    // Créer un nouveau graphique
    chartInstance = new Chart(ctx, {
        type: 'Line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Duration',
                data: data,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

