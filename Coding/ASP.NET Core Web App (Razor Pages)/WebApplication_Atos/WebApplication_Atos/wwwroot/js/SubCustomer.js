let newRowCounter = 1;

function cleanEuro(input) {
    if (!input) return NaN;
    return parseFloat(input.value.replace(/[^\d.,]/g, "").replace(",", ".")) || NaN;
}

function formatEuroInput(input) {
    let value = input.value.replace(/[^\d.,]/g, "").replace(",", ".");
    let number = parseFloat(value);
    input.value = !isNaN(number) ? "€ " + number.toFixed(2) : "";
}

function calculateRow(row) {
    const prijsInput = row.querySelector(".prijs");
    const aantalInput = row.querySelector(".hoeveelheid");
    const inkoopInput = row.querySelector(".inkoopprijs");

    const totaalField = row.querySelector(".totaal");
    const margeField = row.querySelector(".marge");
    const totaalInkoopField = row.querySelector(".totaalinkoop");

    const prijs = cleanEuro(prijsInput);
    const aantal = parseFloat(aantalInput?.value) || NaN;
    const inkoop = cleanEuro(inkoopInput);

    if (!isNaN(prijs) && !isNaN(aantal)) {
        const totaal = prijs * aantal;
        if (totaalField) totaalField.value = "€ " + totaal.toFixed(2);
    } else {
        if (totaalField) totaalField.value = "€ 0.00";
    }

    if (!isNaN(inkoop) && !isNaN(aantal)) {
        const totaalInkoop = inkoop * aantal;
        if (totaalInkoopField) totaalInkoopField.value = "€ " + totaalInkoop.toFixed(2);
    } else {
        if (totaalInkoopField) totaalInkoopField.value = "€ 0.00";
    }

    if (!isNaN(prijs) && !isNaN(inkoop) && inkoop > 0) {
        const marge = ((prijs - inkoop) * 100) / inkoop;
        if (margeField) margeField.value = marge.toFixed(0) + "%";
    } else {
        if (margeField) margeField.value = "0%";
    }

}

function calculateSummary() {
    let totalNetto = 0;
    let totalInkoop = 0;

    document.querySelectorAll("#orderRulesTable tbody tr").forEach(row => {
        const prijs = cleanEuro(row.querySelector(".prijs")) || 0;
        const inkoop = cleanEuro(row.querySelector(".inkoopprijs")) || 0;
        const aantal = parseFloat(row.querySelector(".hoeveelheid")?.value) || 0;

        totalNetto += prijs * aantal;
        totalInkoop += inkoop * aantal;
    });

    const controlegetal = document.getElementById("controlegetal");
    const nettoTotaal = document.getElementById("nettoTotaal");
    const btwInput = document.getElementById("btwPercentage");
    const btwBedrag = document.getElementById("btwBedrag");
    const totaal = document.getElementById("totaal");

    nettoTotaal.value = "€ " + totalNetto.toFixed(2);

    if (totalInkoop > 0) {
        const controle = ((totalNetto - totalInkoop) / totalInkoop) * 100;
        controlegetal.value = controle.toFixed(2) + "%";
    } else {
        controlegetal.value = "0.00%";
    }

    let btw = parseFloat(btwInput?.value || 21);
    if (isNaN(btw) || btw > 100) btw = 100;
    if (btw < 0) btw = 0;
    btwInput.value = btw;

    const btwAmount = (totalNetto * btw) / 100;
    btwBedrag.value = "€ " + btwAmount.toFixed(2);

    totaal.value = "€ " + (totalNetto + btwAmount).toFixed(2);
}

function openItemModal() {
    const modalEl = document.getElementById('addItemModal');
    if (modalEl) {
        const modal = new bootstrap.Modal(modalEl);
        modal.show();
    }
}
function addItemToOrderRules(artikelnummer, omschrijving, prijs, inkoopPrijs, sorteervolgorde, event) {
    if (event) event.preventDefault();

    const tableBody = document.querySelector("#orderRulesTable tbody");
    const index = newRowCounter;

    const hiddenIndex = document.createElement("input");
    hiddenIndex.type = "hidden";
    hiddenIndex.name = "NewOrderRules.index";
    hiddenIndex.value = index;
    tableBody.appendChild(hiddenIndex);
    const row = document.createElement("tr");

    row.innerHTML = `
    <input type="hidden" name="NewOrderRules[${index}].PrijsopgaveId" />
    <td><input name="NewOrderRules[${index}].Artikelnummer" value="${artikelnummer}" readonly /></td>
    <td><input name="NewOrderRules[${index}].Omschrijving" value="${omschrijving}" readonly /></td>
    <td><input name="NewOrderRules[${index}].Hoeveelheid" type="number" class="hoeveelheid" min="1" /></td>
    <td><input name="NewOrderRules[${index}].Prijs" class="prijs euro-input" value="${prijs}" /></td>
    <td><input class="totaal" type="text" readonly /></td>
    <td><input name="NewOrderRules[${index}].Sorteervolgorde" value="${sorteervolgorde}" /></td>
    <td><input class="marge" type="text" readonly /></td>
    <td><input name="NewOrderRules[${index}].InkoopPrijs" class="inkoopprijs euro-input" value="${inkoopPrijs}" /></td>
    <td><input class="totaalinkoop" type="text" readonly /></td>
`;

    tableBody.appendChild(row);

    setTimeout(() => {
        const aantalInput = row.querySelector('.hoeveelheid');
        if (aantalInput) {
            aantalInput.value = 1;
        } else {
            console.warn("❗️Aantal input not found in row.");
        }
    }, 0);

    row.querySelectorAll(".euro-input, .hoeveelheid, .inkoopprijs").forEach(input => {
        if (input.classList.contains("prijs") || input.classList.contains("inkoopprijs")) {
            input.value = "€ " + parseFloat(input.value).toFixed(2);
        }
        input.addEventListener("input", () => {
            calculateRow(row);
            calculateSummary();
        });
    });

    setTimeout(() => {
        calculateRow(row);
        calculateSummary();
    }, 0);
    newRowCounter++;


    const modal = bootstrap.Modal.getInstance(document.getElementById('addItemModal'));
    if (modal) modal.hide();
}

function cleanCurrencyInputs() {
    document.querySelectorAll(".euro-input").forEach(input => {
        input.value = input.value.replace(/[^\d.,-]/g, "").replace(",", ".");
    });
    calculateSummary();
}

document.addEventListener("DOMContentLoaded", () => {
    const table = document.getElementById("orderRulesTable");
    if (!table) return;

    table.querySelectorAll(".euro-input").forEach(input => formatEuroInput(input));

    table.querySelectorAll("tbody tr").forEach(row => {
        calculateRow(row);

        row.querySelectorAll(".hoeveelheid, .prijs, .inkoopprijs").forEach(input => {
            input.addEventListener("input", () => {
                calculateRow(row);
                calculateSummary();
            });
        });
    });

    table.addEventListener("blur", (event) => {
        if (event.target.classList.contains("euro-input")) {
            formatEuroInput(event.target);
        }
    }, true);

    document.getElementById("btwPercentage")?.addEventListener("input", calculateSummary);
    document.addEventListener("input", calculateSummary);

    calculateSummary();
    
});
function cleanCurrencyInputs() {
    document.querySelectorAll(".euro-input").forEach(input => {
        input.value = input.value.replace(/[^\d.,-]/g, "").replace(",", ".");
    });
}