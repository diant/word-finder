export class Word {
    value!: string;
    length!: number;
    points!: number;
    wildchar!: string;
}

export class WordGroup {
    group!: string;
    words!: string[];
}

export class WordRequest {
    letters!: string;
}
