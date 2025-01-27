#!/usr/bin/env dotnet-script

int target = 10;

// int[] nums = int[] { 1, 2, 3, 4 };

List<int> nums = new List<int> { 1, 2, 3, 4 };


// callMe(nums);

// Console.WriteLine($"Final Number Array={String.Join(",", nums.Select(x => x.ToString()).ToArray())}");


Console.WriteLine($"Final Answer: {check(target, nums)}");


void callMe(List<int> nums)
{
    if (nums.Count == 0)
    {
        return;
    }

    Console.WriteLine($"Original Number Array={String.Join(",", nums.Select(x => x.ToString()).ToArray())}");

    List<int> localNums = new List<int>();
    localNums.AddRange(nums);
    localNums.RemoveAt(localNums.Count - 1);

    Console.WriteLine($"After Remove Number Array={String.Join(",", nums.Select(x => x.ToString()).ToArray())}");
    Console.WriteLine($"Local Number Array={String.Join(",", localNums.Select(x => x.ToString()).ToArray())}");
    callMe(localNums);
}


bool check(int target, List<int> nums)
{
	List<int> localNums = new List<int>();
    localNums.AddRange(nums);
	// foreach (int num in nums)
	// {
		// localNums.Add(num);
	// }

	Console.Error.WriteLine($"Starting Check: Target={target} number array {String.Join(",", localNums.Select(x => x.ToString()).ToArray())}");

	if (nums.Count == 0)
	{
        Console.Error.WriteLine("nums count is 0");
		return target == 0;
	}

	if (target <= 0)
	{
        Console.Error.WriteLine("Target value is equal to or less than 0");
        Console.Error.WriteLine($"Target = {target}");
		return false;
	}

	int last = localNums[localNums.Count - 1];
	localNums.RemoveAt(localNums.Count - 1);

	Console.Error.WriteLine($"After Remove Check: Target={target} number array {String.Join(",", localNums.Select(x => x.ToString()).ToArray())}");
    

	return check(target - last, localNums)
            ||
            (target%last==0
            &&
            check((int)Math.Floor((decimal)target/last)
        , localNums));
}

/*
bool check(int target, int[] nums)
{
	Console.WriteLine($"Starting Check: Target={target} number array={String.Join(",", nums.Select(x => x.ToString()).ToArray())}");

	if (nums.Length == 0)
	{
		return target == 0;
	}

	if (target == 0)
	{
		return false;
	}

	int last = nums[nums.Length - 1];

    int[] localNums = new int[nums.Length - 2];
    for (int i = 0; i < nums.Length - 2; i++)
    {
        localNums[i] = nums[i];
    }
    // System.Array.Copy(nums, localNums, nums.Length - 2);

	return check(target-last, localNums) || (target%last==0 && check((int)Math.Floor((decimal)target/last), localNums));
}
*/
