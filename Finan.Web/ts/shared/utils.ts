export class Utils {

    public qs<T extends HTMLElement>(selector: string): T | null {
        return document.querySelector(selector) as T;
    }

    public parseDecimal(value: string | null): number {
        if (!value) return 0;

        value = value.replace(/\./g, '').replace(',', '.');
        const parsed = parseFloat(value);

        return isNaN(parsed) ? 0 : parsed;
    }

    public formatCurrency(value: number): string {
        return value.toLocaleString('pt-BR', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });
    }


    public addDefaultOption(select: HTMLSelectElement, text: string): void {
        const option = new Option(text, "");
        select.add(option);
    }
}