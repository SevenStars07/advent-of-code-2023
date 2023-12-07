// var fileName = "example.txt";

var fileName = "input.txt";

var lettersToValue = new Dictionary<string, int>
{
    { "J", 0 },
    { "2", 1 },
    { "3", 2 },
    { "4", 3 },
    { "5", 4 },
    { "6", 5 },
    { "7", 6 },
    { "8", 7 },
    { "9", 8 },
    { "T", 9 },
    { "Q", 11 },
    { "K", 12 },
    { "A", 13 },
};

var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", fileName))
    .ToList();

var hands = lines.Select(l => (l.Split(" ")[0], int.Parse(l.Split(" ")[1]))).ToList();

var handsRanked = GetHandsRanked(hands.Select(x => x.Item1).ToList());

var total = 0;

for (var i = 0; i < handsRanked.Count; i++)
{
    var hand = handsRanked[i];
    var bid = hands.First(x => x.Item1 == hand).Item2;

    total += bid * (i + 1);
}

Console.WriteLine(total);

List<string> GetHandsRanked(List<string> valueTuples)
{
    var temp = new string[valueTuples.Count];
    valueTuples.CopyTo(temp);

    var result = temp.ToList();
    result.Sort(CompareTwoHands);

    return result;
}

int CompareTwoHands(string hand1, string hand2)
{
    var hand1Freq = GetFreqArray(hand1);
    var hand2Freq = GetFreqArray(hand2);

    var hand1Rank = GetRankOfHand(hand1Freq);
    var hand2Rank = GetRankOfHand(hand2Freq);

    if (hand1Rank > hand2Rank) return 1;
    if (hand1Rank < hand2Rank) return -1;

    for (var i = 0; i < hand1.Length; i++)
    {
        if (lettersToValue[hand1[i].ToString()] > lettersToValue[hand2[i].ToString()]) return 1;
        if (lettersToValue[hand1[i].ToString()] < lettersToValue[hand2[i].ToString()]) return -1;
    }

    return 0;
}

int[] GetFreqArray(string hand)
{
    var freqArray = new int[lettersToValue.Count + 1];

    Array.Fill(freqArray, 0);

    foreach (var c in hand)
    {
        freqArray[lettersToValue[c.ToString()]]++;
    }

    return freqArray;
}

int GetRankOfHand(int[] handFreqArray)
{
    //5 of a kind
    if (handFreqArray.Skip(1).Any(i => i + handFreqArray[0] == 5)) return 7;

    //4 of a kind
    if (handFreqArray.Skip(1).Any(i => i + handFreqArray[0] == 4)) return 6;

    //full house
    if (handFreqArray.Skip(1).Any(v => v == 3) && handFreqArray.Skip(1).Any(v => v == 2)) return 5;
    for (var usedJokers = 1; usedJokers <= handFreqArray[0]; usedJokers++)
    {
        var xIndexes = new List<int>();

        var list = handFreqArray.Skip(1).ToList();

        for (var i = 0; i < list.Count; i++)
        {
            if (list[i] + usedJokers == 3 || list[i] + (handFreqArray[0] - usedJokers) == 2)
                xIndexes.Add(i);
        }

        if (xIndexes.Distinct().Count() == 2) return 5;
    }

    //3 of a kind
    if (handFreqArray.Skip(1).Any(i => i + handFreqArray[0] == 3)) return 4;

    //2 pairs
    if (handFreqArray.Skip(1).Count(i => i == 2) == 2 ||
        (handFreqArray.Skip(1).Count(i => i == 1) == 2 && handFreqArray[0] == 2))
        return 3;

    //1 pair
    if (handFreqArray.Skip(1).Count(i => i == 2) == 1 ||
        (handFreqArray.Skip(1).Any(i => i == 1) && handFreqArray[0] == 1)) return 2;

    return 1;
}