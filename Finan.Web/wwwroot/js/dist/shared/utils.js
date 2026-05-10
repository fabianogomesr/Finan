export class Utils {
    qs(selector) {
        return document.querySelector(selector);
    }
    parseDecimal(value) {
        if (!value)
            return 0;
        value = value.replace(/\./g, '').replace(',', '.');
        const parsed = parseFloat(value);
        return isNaN(parsed) ? 0 : parsed;
    }
    formatCurrency(value) {
        return value.toLocaleString('pt-BR', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }
    addDefaultOption(select, text) {
        const option = new Option(text, "");
        select.add(option);
    }
}
//# sourceMappingURL=utils.js.map