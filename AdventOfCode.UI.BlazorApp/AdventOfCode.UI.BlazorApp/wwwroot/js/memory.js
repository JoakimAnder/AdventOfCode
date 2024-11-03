export function getMemoryUsage() {
    if (performance && performance.memory) {
        let result = {
            UsedHeapSize: performance.memory.usedJSHeapSize,
            TotalHeapSize: performance.memory.totalJSHeapSize,
            HeapSizeLimit: performance.memory.jsHeapSizeLimit
        };

        return JSON.stringify(result);
    }
    return null;
}