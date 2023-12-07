// var fileName = "example.txt";
var fileName = "input.txt";

var lettersToValue = new Dictionary<string, int>
{
    { "2", 1 },
    { "3", 2 },
    { "4", 3 },
    { "5", 4 },
    { "6", 5 },
    { "7", 6 },
    { "8", 7 },
    { "9", 8 },
    { "T", 9 },
    { "J", 10 },
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

    total += (bid * (i + 1));
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
    if (handFreqArray.Any(v => v == 5)) return 7;
    if (handFreqArray.Any(v => v == 4)) return 6;
    if (handFreqArray.Any(v => v == 3) && handFreqArray.Any(v => v == 2)) return 5;
    if (handFreqArray.Any(v => v == 3)) return 4;
    if (handFreqArray.Count(v => v == 2) == 2) return 3;
    if (handFreqArray.Count(v => v == 2) == 1) return 2;

    return 1;
}